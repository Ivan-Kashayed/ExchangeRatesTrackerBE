using ExchangeRatesTracker.App.Interfaces;
using ExchangeRatesTracker.Domain.Entities;
using System.Text.Json;

namespace ExchangeRatesTracker.API.Helpers
{
    public static class ContextPreparationHelpers
    {
        private static JsonSerializerOptions options = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        public static void SeedDb(this IExchangeRatesTrackerContext context)
        {
            context.SeedCurrencies();
        }

        private static void SeedCurrencies(this IExchangeRatesTrackerContext context)
        {
            if (context.Currencies.Any())
            {
                return;
            }

            var currenciesJson = File.ReadAllText("Helpers/Seed/Currencies.json");
            var currencies = JsonSerializer.Deserialize<IEnumerable<Currency>>(currenciesJson, options);

            context.Currencies.AddRange(currencies);

            context.SaveChanges();
        }
    }
}
