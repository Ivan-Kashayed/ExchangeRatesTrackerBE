using ExchangeRatesTracker.App.Models;
using Microsoft.AspNetCore.Mvc;

namespace ExchangeRatesTracker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected ActionResult HandleResponse<T>(ServiceResponse<T> response)
        {
            if (response is null)
            {
                return NotFound();
            }

            if (response.HasErrors)
            {
                return BadRequest(response.Errors);
            }

            return Ok(response.Body);
        }

        protected ActionResult HandleResponse(EmptyServiceResponse response)
        {
            if (response is null)
            {
                return NotFound();
            }

            if (response.HasErrors)
            {
                return BadRequest(response.Errors);
            }

            return Ok(response);
        }
    }
}
