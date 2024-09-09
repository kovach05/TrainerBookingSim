using DataAccess.Context;
using DataImport.Import;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DataImport
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            await RunMigrationAsync(host);
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, config) =>
                {
                    config.AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true);
                })
                .ConfigureServices((context, services) =>
                {
                    services.AddDbContext<ApplicationDbContext>(options =>
                        options.UseNpgsql(context.Configuration.GetConnectionString("DefaultConnection")));

                    services.AddScoped<TrainerImporter>();
                    services.AddScoped<ClientImporter>();
                    services.AddScoped<SubscriptionImporter>();
                    services.AddLogging();
                });

        private static async Task RunMigrationAsync(IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
                var trainerImporter = scope.ServiceProvider.GetRequiredService<TrainerImporter>();
                var clientImporter = scope.ServiceProvider.GetRequiredService<ClientImporter>();
                var subscriptionImporter = scope.ServiceProvider.GetRequiredService<SubscriptionImporter>();

                try
                {
                    await ImportDataAsync(trainerImporter, clientImporter, subscriptionImporter);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An error occurred during the migration process.");
                    Environment.Exit(1);
                }
            }

            Environment.Exit(0);
        }

        private static async Task ImportDataAsync(TrainerImporter trainerImporter, ClientImporter clientImporter, SubscriptionImporter subscriptionImporter)
        {
            await ImportTrainersAsync(trainerImporter);
            await ImportClientsAsync(clientImporter);
            await ImportSubscriptionsAsync(subscriptionImporter);
        }

        private static async Task ImportTrainersAsync(TrainerImporter trainerImporter)
        {
            await ImportFromFileAsync(trainerImporter, "trainers.json", "json");
            await ImportFromFileAsync(trainerImporter, "trainers.xml", "xml");
        }

        private static async Task ImportClientsAsync(ClientImporter clientImporter)
        {
            await ImportFromFileAsync(clientImporter, "clients.json", "json");
            await ImportFromFileAsync(clientImporter, "clients.xml", "xml");
        }

        private static async Task ImportSubscriptionsAsync(SubscriptionImporter subscriptionImporter)
        {
            await ImportFromFileAsync(subscriptionImporter, "subscriptions.json", "json");
            await ImportFromFileAsync(subscriptionImporter, "subscriptions.xml", "xml");
        }

        private static async Task ImportFromFileAsync<T>(T importer, string filePath, string fileType) where T : class
        {
            var method = importer.GetType().GetMethod("ImportAsync", new[] { typeof(string), typeof(string) });
            if (method != null)
            {
                await (Task)method.Invoke(importer, new object[] { filePath, fileType });
            }
        }
    }
}
