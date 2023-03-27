using ExchangeRatesTracker.App.DTOs;
using ExchangeRatesTracker.App.Services;
using Microsoft.AspNetCore.Mvc;

namespace ExchangeRatesTracker.API.Controllers
{
    public class CurrencyController : BaseController
    {
        private readonly ICurrencyService _currencyService;

        public CurrencyController(ICurrencyService currencyService)
        {
            _currencyService = currencyService;
        }

        [HttpGet("v1")]
        [ProducesResponseType(typeof(IEnumerable<CurrencyDTO>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetCurrencies()
        {
            var result = await _currencyService.GetCurrenciesAsync();
            return HandleResponse(result);
        }
    }
}
