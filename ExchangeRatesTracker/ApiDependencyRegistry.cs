using ExchangeRatesTracker.API.HostedServices;
using ExchangeRatesTracker.App.DataManagement.CzechNationalBank;
using Microsoft.OpenApi.Models;

namespace ExchangeRatesTracker.API
{
    public static class ApiDependencyRegistry
    {
        public static IServiceCollection AddApiDependencies(this IServiceCollection services, IConfiguration config)
        {
            services.AddControllers();

            services.AddCors();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ExchangeRateTrackerService", Version = "v1" });
            });

            services.AddSingleton<CzechNationalBankService>();
            services.AddHostedService<CNBDataManagementHostetService>();

            return services;
        }
    }
}
