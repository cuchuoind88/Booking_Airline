using Booking_Airline.DTOs.AirportDTOs;
using Booking_Airline.DTOs.CountryDTOs;
using Booking_Airline.Serivces.ServiceManager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Booking_Airline.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AirportController : ControllerBase
    {
        private readonly IServiceManger serviceManger;
        public AirportController(IServiceManger serviceManger)
        {
            this.serviceManger = serviceManger;
        }
        [HttpPost]
        [Route("/airportes/{countryId}/CreateAirport")]
        public async Task<IActionResult> CreateAirport (Guid countryId  , [FromBody] AirportCreateDTOs airportCreateDTOs)
        {
          if(airportCreateDTOs is null)
                return BadRequest("AirportForCreationDto object is null");
          var createdAiport= await serviceManger.AirportService.CreateAirport(countryId ,airportCreateDTOs);
            return Ok(createdAiport);
        }
        [HttpPost]
        [Route("/airportes/{countryId}/CreateAirportCollection")]
        public async Task<IActionResult> CreateAirportColection ( Guid countryId , [FromBody] IEnumerable<AirportCreateDTOs> airportCreateDTOs)
        {
            var result = await serviceManger.AirportService.CreateAirportCollection(countryId, airportCreateDTOs);
            return Ok(result);
        }
    }
}
