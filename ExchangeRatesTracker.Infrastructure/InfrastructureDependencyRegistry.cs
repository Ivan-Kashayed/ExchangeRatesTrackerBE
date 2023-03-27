using ExchangeRatesTracker.App.Interfaces;
using ExchangeRatesTracker.Infrastructure.Integration;
using ExchangeRatesTracker.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ExchangeRatesTracker.Infrastructure
{
    public static class InfrastructureDependencyRegistry
    {
        public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection services, IConfiguration config)
        {
            var connectionString = config.GetConnectionString("DefaultConnection");
            services.AddDbContext<ExchangeRatesTrackerContext>(options => options.UseSqlServer(connectionString));

            services.AddScoped<IExchangeRatesTrackerContext>(x => x.GetRequiredService<ExchangeRatesTrackerContext>());
            services.AddSingleton<ICzechNationalBankHttpClient, CzechNationalBankHttpClient>();

            services.AddHttpClient();

            return services;
        }
    }
}
