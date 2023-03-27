using AutoMapper;
using ExchangeRatesTracker.App.DTOs;
using ExchangeRatesTracker.Domain.Entities;

namespace ExchangeRatesTracker.App.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Currency, CurrencyDTO>();
            CreateMap<ExchangeRate, ExchangeRateDTO>();
        }
    }
}
