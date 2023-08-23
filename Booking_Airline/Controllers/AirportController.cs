using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Booking_Airline.Controllers
{
    [ApiController]
    public class AirportController : Controller
    {
        [HttpGet]
        [Route("/airport")]
        [Authorize(Roles ="User, Admin")]
        public IActionResult GetListAirport()
        {
            return new OkObjectResult("OK , SUCCESSFULL");
        }
    }
}
