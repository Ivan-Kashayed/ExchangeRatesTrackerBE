using AutoMapper;
using ExchangeRatesTracker.App.DTOs;
using ExchangeRatesTracker.App.Interfaces;
using ExchangeRatesTracker.App.Models;
using Microsoft.EntityFrameworkCore;

namespace ExchangeRatesTracker.App.Services
{
    public class ExchangeRateService : IExchangeRateService
    {
        private readonly IExchangeRatesTrackerContext _context;
        private readonly IMapper _mapper;

        public ExchangeRateService(IExchangeRatesTrackerContext exchangeRatesTrackerContext, IMapper mapper)
        {
            _context = exchangeRatesTrackerContext;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<ExchangeRateDTO>> GetExchangeRate(string currencyCode, DateTime dateTime)
        {
            dateTime = dateTime.Date;

            if(dateTime > DateTime.UtcNow.Date)
            {
                return new("We don't know the future..");
            }

            var exchangeRate = await _context.ExchangeRates.FirstOrDefaultAsync(r => r.СurrencyCode == currencyCode && r.Date == dateTime);

            if (exchangeRate == null)
            {
                exchangeRate = await _context.ExchangeRates
                        .Where(r => r.СurrencyCode == currencyCode && r.Date < dateTime)
                        .OrderByDescending(r => r.Date)
                        .FirstOrDefaultAsync();
            }

            return exchangeRate == null ? 
                new("Exchange rate doesn't exist") : 
                new(_mapper.Map<ExchangeRateDTO>(exchangeRate));
        }
    }
}
