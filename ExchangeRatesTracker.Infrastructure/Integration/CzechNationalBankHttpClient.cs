using ExchangeRatesTracker.App.Configuration;
using ExchangeRatesTracker.App.DataManagement.CzechNationalBank.Entities;
using ExchangeRatesTracker.App.DataManagement.CzechNationalBank.Extensions;
using ExchangeRatesTracker.App.Interfaces;
using Microsoft.Extensions.Options;

namespace ExchangeRatesTracker.Infrastructure.Integration
{
    public class CzechNationalBankHttpClient : ICzechNationalBankHttpClient
    {
        private readonly string _cnbExchangeRateUrl;
        private readonly HttpClient _httpClient;

        public CzechNationalBankHttpClient(IOptions<CNBConfiguration> cnbConfigurationOpions, HttpClient httpClient)
        {
            _cnbExchangeRateUrl = cnbConfigurationOpions.Value.ExchangeRateUrl;

            _httpClient = httpClient;
        }

        public async Task<IEnumerable<CzechNationalBankExchangeRate>> GetExchangeRates(int year, CancellationToken cancellationToken)
        {
            var url = _cnbExchangeRateUrl + year;

            var response = await _httpClient.GetAsync(url, cancellationToken);
            response.EnsureSuccessStatusCode();

            return (await response.Content.ReadAsStringAsync()).ParseResponse();
        }
    }
}
