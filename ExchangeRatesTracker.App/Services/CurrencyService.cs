using AutoMapper;
using AutoMapper.QueryableExtensions;
using ExchangeRatesTracker.App.DTOs;
using ExchangeRatesTracker.App.Interfaces;
using ExchangeRatesTracker.App.Models;
using Microsoft.EntityFrameworkCore;

namespace ExchangeRatesTracker.App.Services
{
    public class CurrencyService : ICurrencyService
    {
        private readonly IExchangeRatesTrackerContext _context;
        private readonly IMapper _mapper;

        public CurrencyService(IExchangeRatesTrackerContext exchangeRatesTrackerContext, IMapper mapper)
        {
            _context = exchangeRatesTrackerContext;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<IEnumerable<CurrencyDTO>>> GetCurrenciesAsync()
        {
            var result = await _context.Currencies.Where(c => c.MinDate != default(DateTime)).ProjectTo<CurrencyDTO>(_mapper.ConfigurationProvider).ToListAsync();
            return new(result);
        }
    }
}
