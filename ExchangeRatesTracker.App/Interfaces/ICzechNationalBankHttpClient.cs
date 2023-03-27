using ExchangeRatesTracker.App.DataManagement.CzechNationalBank.Entities;

namespace ExchangeRatesTracker.App.Interfaces
{
    public interface ICzechNationalBankHttpClient
    {
        Task<IEnumerable<CzechNationalBankExchangeRate>> GetExchangeRates(int year, CancellationToken cancellationToken);
    }
}