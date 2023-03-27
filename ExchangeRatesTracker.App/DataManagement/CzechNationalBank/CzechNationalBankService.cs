using ExchangeRatesTracker.App.DataManagement.CzechNationalBank.Entities;
using ExchangeRatesTracker.App.Interfaces;
using ExchangeRatesTracker.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ExchangeRatesTracker.App.DataManagement.CzechNationalBank
{
    public class CzechNationalBankService
    {
        private readonly ICzechNationalBankHttpClient _czechNationalBankHttpClient;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        private const int minYear = 1991;

        public CzechNationalBankService(
            ICzechNationalBankHttpClient czechNationalBankHttpClient,
            IServiceScopeFactory serviceScopeFactory
        )
        {
            _czechNationalBankHttpClient = czechNationalBankHttpClient;
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task SyncAsync(CancellationToken cancellationToken)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<IExchangeRatesTrackerContext>();

            var lastRate = await context.ExchangeRates.OrderByDescending(r => r.Date).FirstOrDefaultAsync();
            DateTime dateFrom;

            if (lastRate == null)
            {
                dateFrom = new DateTime(minYear, 1, 1);
            }
            else
            {
                dateFrom = lastRate.Date;
            }

            var years = Enumerable.Range(dateFrom.Year, DateTime.Now.Year - dateFrom.Year + 1);

            var currencies = await context.Currencies.ToListAsync();

            foreach (var year in years)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    return;
                }

                var cnbExchangeRates = await _czechNationalBankHttpClient.GetExchangeRates(year, cancellationToken);

                if (year == years.First())
                {
                    cnbExchangeRates = cnbExchangeRates.Where(r => r.Date > dateFrom.Date);
                }

                AddExchangeRates(currencies, cnbExchangeRates, context, cancellationToken);
                
                //as can be to many records to save
                await context.SaveChangesAsync(cancellationToken);
            }

            await UpdateCurrenciesDateRanges(currencies, context);
            await context.SaveChangesAsync(cancellationToken);
        }

        private void AddExchangeRates(
            List<Currency> currencies,
            IEnumerable<CzechNationalBankExchangeRate> cnbExchangeRates, 
            IExchangeRatesTrackerContext context, 
            CancellationToken cancellationToken)
        {
            foreach (var cnbRate in cnbExchangeRates)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    return;
                }

                var currency = currencies.FirstOrDefault(c => c.Code == cnbRate.CzechNationalBankCurrency.Code);

                if (currency is null)
                {
                    currency = new Currency()
                    {
                        Code = cnbRate.CzechNationalBankCurrency.Code,
                        Name = "Unknown"
                    };

                    context.Currencies.Add(currency);
                    currencies.Add(currency);
                }

                var exchangeRate = new ExchangeRate()
                {
                    Сurrency = currency,
                    Date = cnbRate.Date,
                    Rate = cnbRate.Rate / cnbRate.CzechNationalBankCurrency.Quantity
                };

                context.ExchangeRates.Add(exchangeRate);
            }
        }

        private async Task UpdateCurrenciesDateRanges(List<Currency> currencies, IExchangeRatesTrackerContext context)
        {
            foreach(var currency in currencies)
            {
                currency.MinDate = await context.ExchangeRates
                    .Where(c => c.СurrencyCode == currency.Code)
                    .OrderBy(c => c.Date)
                    .Select(c => c.Date)
                    .FirstOrDefaultAsync();
                currency.MaxDate = await context.ExchangeRates
                    .Where(c => c.СurrencyCode == currency.Code)
                    .OrderByDescending(c => c.Date)
                    .Select(c => c.Date)
                    .FirstOrDefaultAsync();

                // possible can be 7 days off :)
                if(currency.MaxDate.AddDays(7) > DateTime.UtcNow.Date)
                {
                    currency.MaxDate = DateTime.UtcNow.Date;
                }

            }
        }
    }
}
