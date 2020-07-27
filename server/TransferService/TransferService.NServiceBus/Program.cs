using AutoMapper;
using Messages.Commands;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NServiceBus;
using NServiceBus.Persistence.Sql;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Threading.Tasks;
using TransferService.Contract;
using TransferService.Data;

namespace TransferService.NServiceBus
{
    class Program
    {
        static async Task Main(string[] args)
        {
            const string endpointName = "Bank.Transfer";
            Console.Title = endpointName;

            var endpointConfiguration = new EndpointConfiguration(endpointName);
            endpointConfiguration.EnableInstallers();
            //if in development
            endpointConfiguration.PurgeOnStartup(true);

            var appSettings = ConfigurationManager.AppSettings;
            string transferConnection = ConfigurationManager.ConnectionStrings["TransferConnectionString"].ToString();
            var transportConnection = ConfigurationManager.ConnectionStrings["TransportConnection"].ToString();
            var auditQueue = appSettings.Get("AuditQueue");
            var userEndpoint = appSettings.Get("UserEndpoint");
            var schemaName = appSettings.Get("SchemaName");
            var tablePrefix = appSettings.Get("TablePrefix");
            var serviceControlQueue = appSettings.Get("ServiceControlQueue");
            var timeToBeReceivedSetting = appSettings.Get("TimeToBeReceived");
            var timeToBeReceived = TimeSpan.Parse(timeToBeReceivedSetting);

            var containerSettings = endpointConfiguration.UseContainer(new DefaultServiceProviderFactory());
            containerSettings.ServiceCollection.AddScoped(typeof(ITransferRepository), typeof(TransferRepository));
            containerSettings.ServiceCollection.AddAutoMapper(typeof(Program));
            using (var transferDataContext = new TransferDbContext(new DbContextOptionsBuilder<TransferDbContext>()
                .UseSqlServer(new SqlConnection(transferConnection))
                .Options))
            {
                await transferDataContext.Database.EnsureCreatedAsync().ConfigureAwait(false);
            }
          
            var persistence = endpointConfiguration.UsePersistence<SqlPersistence>();
            persistence.SqlDialect<SqlDialect.MsSqlServer>();
            persistence.ConnectionBuilder(
                connectionBuilder: () =>
                {
                    return new SqlConnection(transferConnection);
                });

            var dialect = persistence.SqlDialect<SqlDialect.MsSqlServer>();
            dialect.Schema(schemaName);

            var outboxSettings = endpointConfiguration.EnableOutbox();
            outboxSettings.KeepDeduplicationDataFor(TimeSpan.FromDays(6));
            outboxSettings.RunDeduplicationDataCleanupEvery(TimeSpan.FromMinutes(15));

            var transport = endpointConfiguration.UseTransport<RabbitMQTransport>();
            transport.UseConventionalRoutingTopology()
                .ConnectionString(transportConnection);

            var routing = transport.Routing();
            routing.RouteToEndpoint(
                        messageType: typeof(ICommitTransfer),
                        destination: userEndpoint);

            var recoverability = endpointConfiguration.Recoverability();
            recoverability.Delayed(
                customizations: delayed =>
                {
                    delayed.NumberOfRetries(0);
                    delayed.TimeIncrease(TimeSpan.FromMinutes(3));
                });

            recoverability.Immediate(
               customizations: delayed =>
               {
                   delayed.NumberOfRetries(1);

               });

            var conventions = endpointConfiguration.Conventions();
            conventions.DefiningCommandsAs(type => type.Namespace == "Messages.Commands");
            conventions.DefiningEventsAs(type => type.Namespace == "Messages.Events");
            conventions.DefiningMessagesAs(type => type.Namespace == "Messages.Messages");

            var subscriptions = persistence.SubscriptionSettings();
            subscriptions.CacheFor(TimeSpan.FromMinutes(10));

            endpointConfiguration.AuditProcessedMessagesTo(
                          auditQueue: auditQueue,
                           timeToBeReceived: timeToBeReceived);
            endpointConfiguration.AuditSagaStateChanges(
                           serviceControlQueue: "Particular.Servicecontrol");

            endpointConfiguration.RegisterComponents(c =>
            {
                c.ConfigureComponent(b =>
                {
                    var session = b.Build<ISqlStorageSession>();

                    var context = new TransferDbContext(new DbContextOptionsBuilder<TransferDbContext>()
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

