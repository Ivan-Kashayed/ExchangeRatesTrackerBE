using ExchangeRatesTracker.App.DTOs;
using ExchangeRatesTracker.App.Models;

namespace ExchangeRatesTracker.App.Services
{
    public interface IExchangeRateService
    {
        Task<ServiceResponse<ExchangeRateDTO>> GetExchangeRate(string currencyCode, DateTime dateTime);
    }
}