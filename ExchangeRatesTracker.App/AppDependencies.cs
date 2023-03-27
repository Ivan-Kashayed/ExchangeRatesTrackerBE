using ExchangeRatesTracker.App.Configuration;
using ExchangeRatesTracker.App.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ExchangeRatesTracker.App
{
    public static class AppDependencies
    {
        public static IServiceCollection AddAppDependencies(this IServiceCollection services, IConfiguration config)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddScoped<ICurrencyService, CurrencyService>();
            services.AddScoped<IExchangeRateService, ExchangeRateService>();

            services.Configure<CNBConfiguration>(options => config.GetSection("CzechNationalBank").Bind(options));

            return services;
        }
    }
}
