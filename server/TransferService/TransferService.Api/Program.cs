using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using NServiceBus;
using Serilog;
using System;
using System.Diagnostics;
using System.IO;

namespace TransferService.Api
{
    public class Program
    {
        public static IConfiguration Configuration { get; } = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
               .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
               .Build();

        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(Configuration)
                 .CreateLogger();

            Serilog.Debugging.SelfLog.Enable(msg =>
            {
                Debug.Print(msg);
                Debugger.Break();
            });

            try
            {
                Log.Information("The program has started!!!");

                CreateHostBuilder(args).Build().Run();


            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
            }
            finally
            {

                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                  .UseNServiceBus(context =>
                  {

                      const string endpointName = "Bank.Transfer.Api";
                      var endpointConfiguration = new EndpointConfiguration(endpointName);

                      endpointConfiguration.EnableInstallers();

                      endpointConfiguration.SendOnly();

                      var scanner = endpointConfiguration.AssemblyScanner();
                      scanner.ExcludeAssemblies("TransferService.Data.dll");

                      var outboxSettings = endpointConfiguration.EnableOutbox();

                      outboxSettings.KeepDeduplicationDataFor(TimeSpan.FromDays(6));
                      outboxSettings.RunDeduplicationDataCleanupEvery(TimeSpan.FromMinutes(15));

                      var auditQueue = Configuration["AppSettings:auditQueue"];
                      //var auditQueue = appSettings.Get("auditQueue");
                      // var serviceControlQueue = appSettings.Get("ServiceControlQueue");
                      var serviceControlQueue = Configuration["AppSettings:ServiceControlQueue"];
                      var measureEndpoint = Configuration["AppSettings:MeasureEndpoint"];

                      var timeToBeReceivedSetting = Configuration["AppSettings:timeToBeReceived"];
                      var subscriberEndpoint = Configuration["AppSettings:SubscriberEndpoint"];
                      var transportConnection = Configuration.GetConnectionString("transportConnection");
                      var schemaName = Configuration["AppSettings:SchemaName"];
                      var timeToBeReceived = TimeSpan.Parse(timeToBeReceivedSetting);
                      endpointConfiguration.AuditProcessedMessagesTo(
                          auditQueue: auditQueue,
                          timeToBeReceived: timeToBeReceived);

                      var recoverability = endpointConfiguration.Recoverability();
                      recoverability.Delayed(
                          customizations: delayed =>
                          {
                              delayed.NumberOfRetries(0);
                              delayed.TimeIncrease(TimeSpan.FromMinutes(4));
                          });

                      recoverability.Immediate(
                          customizations: immidiate =>
                          {
                              immidiate.NumberOfRetries(1);

                          });

                      var transport = endpointConfiguration.UseTransport<RabbitMQTransport>();
                      transport.UseConventionalRoutingTopology()
                          .ConnectionString(transportConnection);


                      var persistence = endpointConfiguration.UsePersistence<SqlPersistence>();
                      var connection = Configuration.GetConnectionString("BankTransferDBConnectionString");

                      var dialect = persistence.SqlDialect<SqlDialect.MsSqlServer>();
                      dialect.Schema(schemaName);

                      persistence.ConnectionBuilder(
                          connectionBuilder: () =>
                          {
                              return new SqlConnection(connection);
                          });

                      var subscriptions = persistence.SubscriptionSettings();
                      subscriptions.CacheFor(TimeSpan.FromMinutes(10));


                      //in development
                      // endpointConfiguration.PurgeOnStartup(true);


/*                      endpointConfiguration.AuditSagaStateChanges(
                          serviceControlQueue: "Particular.Servicecontrol");*/

                      var conventions = endpointConfiguration.Conventions();
                      conventions.DefiningCommandsAs(type => type.Namespace == "Messages.Commands");
                      conventions.DefiningEventsAs(type => type.Namespace == "Messages.Events");
                      conventions.DefiningMessagesAs(type => type.Namespace == "Messages.Messages");


                      return endpointConfiguration;
                  })
                 .ConfigureWebHostDefaults(webBuilder =>
                 {
                     webBuilder.UseStartup<Startup>();
                 });
    }
}

