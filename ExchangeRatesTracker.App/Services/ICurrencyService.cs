using ExchangeRatesTracker.App.DTOs;
using ExchangeRatesTracker.App.Models;

namespace ExchangeRatesTracker.App.Services
{
    public interface ICurrencyService
    {
        Task<ServiceResponse<IEnumerable<CurrencyDTO>>> GetCurrenciesAsync();
    }
}