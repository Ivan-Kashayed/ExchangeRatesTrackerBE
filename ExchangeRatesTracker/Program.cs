using ExchangeRatesTracker.API.Helpers;
using ExchangeRatesTracker.App.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ExchangeRatesTracker.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            using var scope = host.Services.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<IExchangeRatesTrackerContext>();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

            try
            {
                context.Database.Migrate();

                logger.LogInformation("Start seeding database data");
                context.SeedDb();
                logger.LogInformation("Seeding completed successfully");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occurred during migration or seeding");
            }

            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}