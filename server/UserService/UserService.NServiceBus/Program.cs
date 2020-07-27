using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NServiceBus;
using NServiceBus.Persistence.Sql;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Threading.Tasks;
using UserService.Contract;
using UserService.Data;

namespace UserService.NServiceBus
{
    class Program
    {
        static async Task Main(string[] args)
        {
            const string EndPointName = "Bank.User";
            Console.Title = EndPointName;

            var endpointConfiguration = new EndpointConfiguration(EndPointName);
            endpointConfiguration.PurgeOnStartup(true);
            endpointConfiguration.EnableInstallers();

            var appSettings = ConfigurationManager.AppSettings;
            var userConnection = ConfigurationManager.ConnectionStrings["userConnectionString"].ToString();
            var transportConnection = ConfigurationManager.ConnectionStrings["transportConnection"].ToString();
            var schemaName = appSettings.Get("SchemaName");
            var tablePrefix = appSettings.Get("TablePrefix");
            var auditQueue = appSettings.Get("auditQueue");
            var timeToBeReceivedSetting = appSettings.Get("timeToBeReceived");
            var timeToBeReceived = TimeSpan.Parse(timeToBeReceivedSetting);

            var containerSettings = endpointConfiguration.UseContainer(new DefaultServiceProviderFactory());
            containerSettings.ServiceCollection.AddScoped(typeof(IUserRepository), typeof(UserRepository));
            containerSettings.ServiceCollection.AddAutoMapper(typeof(Program));
            using (var userDataContext = new UserDbContext(new DbContextOptionsBuilder<UserDbContext>()
                .UseSqlServer(new SqlConnection(userConnection)).Options))
            {
                await userDataContext.Database.EnsureCreatedAsync();
            }

            var persistence = endpointConfiguration.UsePersistence<SqlPersistence>();
            persistence.TablePrefix(tablePrefix);
            persistence.ConnectionBuilder(
               connectionBuilder: () =>
               {
                   return new SqlConnection(userConnection);
               });

            var dialect = persistence.SqlDialect<SqlDialect.MsSqlServer>();
            dialect.Schema(schemaName);

            var outboxSettings = endpointConfiguration.EnableOutbox();
            outboxSettings.KeepDeduplicationDataFor(TimeSpan.FromDays(6));
            outboxSettings.RunDeduplicationDataCleanupEvery(TimeSpan.FromMinutes(15));
            SubscribeToNotifications.Subscribe(endpointConfiguration);

            var transport = endpointConfiguration.UseTransport<RabbitMQTransport>();
            transport.UseConventionalRoutingTopology()
                .ConnectionString(transportConnection);

            var conventions = endpointConfiguration.Conventions();
            conventions.DefiningCommandsAs(type => type.Namespace == "Messages.Commands");
            conventions.DefiningMessagesAs(type => type.Namespace == "Messages.Messages");
            conventions.DefiningEventsAs(type => type.Namespace == "Messages.Events");

            var recoverability = endpointConfiguration.Recoverability();
            recoverability.CustomPolicy(UserServiceRetryPolicy.UserServiceRetryPolicyInvoke);

            var subscriptions = persistence.SubscriptionSettings();
            subscriptions.CacheFor(TimeSpan.FromMinutes(10));

            endpointConfiguration.AuditProcessedMessagesTo(
                auditQueue: auditQueue,
                timeToBeReceived: timeToBeReceived);

            endpointConfiguration.RegisterComponents(c =>
            {
                c.ConfigureComponent(b =>
                {
                    var session = b.Build<ISqlStorageSession>();
                    var context = new UserDbContext(new DbContextOptionsBuilder<UserDbContext>()
                        .UseSqlServer(session.Connection)
                        .Options);
                    //Use the same underlying ADO.NET transaction
                    context.Database.UseTransaction(session.Transaction);
                    //Ensure context is flushed before the transaction is committed
                    session.OnSaveChanges(s => context.SaveChangesAsync());
                    return context;
                }, DependencyLifecycle.InstancePerUnitOfWork);
            });

            var endpointInstance = await Endpoint.Start(endpointConfiguration);

            Console.WriteLine("Press Enter to exit.");
            Console.ReadLine();

            await endpointInstance.Stop();
        }
    }
}
