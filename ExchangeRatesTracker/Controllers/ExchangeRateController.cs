using ExchangeRatesTracker.App.DTOs;
using ExchangeRatesTracker.App.Services;
using Microsoft.AspNetCore.Mvc;

namespace ExchangeRatesTracker.API.Controllers
{
    public class ExchangeRateController : BaseController
    {
        private readonly IExchangeRateService _exchangeRateService;

        public ExchangeRateController(IExchangeRateService exchangeRateService) 
        {
            _exchangeRateService = exchangeRateService;
        }

        [HttpGet("v1/{currencyCode}/{dateTime}")]
        [ProducesResponseType(typeof(IEnumerable<ExchangeRateDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> GetExchangeRate(string currencyCode, DateTime dateTime)
        {
            var result = await _exchangeRateService.GetExchangeRate(currencyCode, dateTime);
            return HandleResponse(result);
        }
    }
}
