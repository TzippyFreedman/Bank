using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NServiceBus;
using NServiceBus.Persistence.Sql;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Threading.Tasks;
using UserService.Data;
using UserService.Services.Interfaces;

namespace UserService.NServiceBus
{
    class Program
    {
        static async Task Main(string[] args)
        {
            const string EndPointName = "Bank.User";

            var appSettings = ConfigurationManager.AppSettings;

            Console.Title = EndPointName;
            var endpointConfiguration = new EndpointConfiguration(EndPointName);
            endpointConfiguration.PurgeOnStartup(true);

            endpointConfiguration.EnableInstallers();

            var persistence = endpointConfiguration.UsePersistence<SqlPersistence>();
            var connection = ConfigurationManager.ConnectionStrings["userConnectionString"].ToString();


            var dialect = persistence.SqlDialect<SqlDialect.MsSqlServer>();
            var SchemaName = appSettings.Get("SchemaName");
            var tablePrefix = appSettings.Get("TablePrefix");
            dialect.Schema(SchemaName);
            persistence.TablePrefix(tablePrefix);
          
            var containerSettings = endpointConfiguration.UseContainer(new DefaultServiceProviderFactory());
            using (var receiverDataContext = new UserDbContext(new DbContextOptionsBuilder<UserDbContext>()
          .UseSqlServer(new SqlConnection(connection))
          .Options))
            {
                await receiverDataContext.Database.EnsureCreatedAsync().ConfigureAwait(false);
            }
            //containerSettings.ServiceCollection.AddDbContext<UserDbContext>
            //(options => options
            //.UseSqlServer(connection));
         //   containerSettings.ServiceCollection.AddSingleton(typeof(ITransferMoneyHandlerRepository), typeof(UserAccountHandlerRepository));

            //containerSettings.ServiceCollection.AddSingleton(typeof(IUserService), typeof(UserService.Services.UserService));
            //containerSettings.ServiceCollection.AddSingleton(typeof(IUserRepository), typeof(UserRepository));
            containerSettings.ServiceCollection.AddAutoMapper(typeof(Program));
            var outboxSettings = endpointConfiguration.EnableOutbox();

            outboxSettings.KeepDeduplicationDataFor(TimeSpan.FromDays(6));
            outboxSettings.RunDeduplicationDataCleanupEvery(TimeSpan.FromMinutes(15));
            SubscribeToNotifications.Subscribe(endpointConfiguration);

            //var containerBuilder = new ContainerBuilder();
            //containerBuilder.Register(_ => endpointInstance).InstancePerDependency();

            //SubscribeToNotifications.Subscribe(endpointConfiguration);

            //containerBuilder.Register(ctx => CreateMessageSession( ctx.Resolve<ILifetimeScope>()))
            //       .As<IMessageSession>()
            //       .SingleInstance();
            var transport = endpointConfiguration.UseTransport<RabbitMQTransport>();
            var RabbitmqConnection = ConfigurationManager.ConnectionStrings["localRabbitmqConnectionString"].ToString();

            transport.UseConventionalRoutingTopology()
                .ConnectionString(RabbitmqConnection);
            var routing = transport.Routing();
            //routing.RouteToEndpoint(
            //    assembly: typeof(ICheckBalance).Assembly,
            //    destination: "UserService"
            //    );
            //var TrackingEndPoint = appSettings.Get("TrackingEndPoint");

            //routing.RouteToEndpoint(
            //  messageType: typeof(ICancelTransfer),
            //  destination: TransferEndPoint
            //  );

            //see successed messages in serviceInsight
            var auditQueue = appSettings.Get("auditQueue");
            var timeToBeReceivedSetting = appSettings.Get("timeToBeReceived");
            var timeToBeReceived = TimeSpan.Parse(timeToBeReceivedSetting);
            endpointConfiguration.AuditProcessedMessagesTo(
                auditQueue: auditQueue,
                timeToBeReceived: timeToBeReceived);
            //show the saga flow in serviceinsight


            endpointConfiguration.AuditSagaStateChanges(
serviceControlQueue: "Particular.Servicecontrol");
            var conventions = endpointConfiguration.Conventions();
            conventions.DefiningCommandsAs(type => type.Namespace == "Messages.Commands");
            conventions.DefiningMessagesAs(type => type.Namespace == "Messages.Messages");

            conventions.DefiningEventsAs(type => type.Namespace == "Messages.Events");
            //var subscriptions = transport.SubscriptionSettings();
            //subscriptions.CacheSubscriptionInformationFor(TimeSpan.FromMinutes(1));

            //subscriptions.SubscriptionTableName(
            //    tableName: "Subscriptions",
            //    schemaName: "dbo");
            persistence.ConnectionBuilder(
                connectionBuilder: () =>
                {
                    return new SqlConnection(connection);
                });

            var recoverability = endpointConfiguration.Recoverability();
            recoverability.CustomPolicy(UserServiceRetryPolicy.UserServiceRetryPolicyInvoke);
            recoverability.Immediate(
                          immediate => {
                              immediate.NumberOfRetries(1);
                          });
            recoverability.Delayed(
                delayed =>
                {
                    var retries = delayed.NumberOfRetries(1);
                    retries.TimeIncrease(TimeSpan.FromSeconds(2));
                });

            var subscriptions = persistence.SubscriptionSettings();
            subscriptions.CacheFor(TimeSpan.FromMinutes(1));
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
            var endpointInstance = await Endpoint.Start(endpointConfiguration)
             .ConfigureAwait(false);

            Console.WriteLine("Press Enter to exit.");
            Console.ReadLine();

            await endpointInstance.Stop()
                .ConfigureAwait(false);
        }
    }
}
