using Booking_Airline.Models;
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
        [HttpGet]
        [Route("/airport/GetAirports/{Id}")]
        public async Task<IActionResult> GetAirport( int Id)
        {
            var result = await airportRepository.GetAirport(Id);
            return result;
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("/airport/CreateAirport")]
        public async Task<IActionResult> CreateAirport([FromBody] Airport airport)
        {
            var result = await airportRepository.CreateAirport(airport);
            return result;
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete]
        [Route("/airport/DeleteAirport/{Id}")]
        public async Task<IActionResult> DeleteAirport( int Id)
        {
            var result = await airportRepository.DeleteAirport(Id);
            return result;
        }
        [Authorize(Roles = "Admin")]
        [HttpPut]
        [Route("/airport/UpdateAirport/{Id}")]
        public async Task<IActionResult> UpdateAirport(int Id, [FromBody] Airport airport)
        {
            var result = await airportRepository.UpdateAirport(Id,airport);
            return result;
        }
    }
}
