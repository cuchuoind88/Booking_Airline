using Booking_Airline.Repository.AirportService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Booking_Airline.Controllers
{
    [ApiController]
    public class AirportController : Controller
    {   private readonly IAirportRepository airportRepository;
        public AirportController(IAirportRepository airportRepository)
        {
             this.airportRepository = airportRepository;
        }
        [HttpGet]
        [Route("/airport/GetAirports")]
        public async Task<IActionResult> GetListAirport()
        {
            var result=await airportRepository.GetAllAirport();
            return result;
        }
    }
}
