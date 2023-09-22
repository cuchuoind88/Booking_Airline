using Booking_Airline.DTOs.CountryDTOs;
using Booking_Airline.Serivces.ServiceManager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Booking_Airline.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly IServiceManger serviceManger;
        public CountriesController ( IServiceManger serviceManger )
        {
            this.serviceManger = serviceManger;
        }
        [HttpPost]
        //Remembar to add Decentralize
        [Route("/countries/CreateCountry")]
        public async Task<IActionResult> CreateCountry([FromBody]CountryCreateDTOs country)
        {
            if( country is null)
            {
                return BadRequest("CountryForCreationDto object is null");

            }
            var createdCountry=await serviceManger.CountriesSerivce.CreateCountry(country);
            return Ok(createdCountry);
        }
        [HttpGet]
        [Route("/countries/GetAllCountry")]
        public async Task<IActionResult> GetAllCountry()
        {
           return Ok(await serviceManger.CountriesSerivce.GetAllContries());
        }
        //Create Collection Country
        [HttpPost]
        //Remembar to add Decentralize
        [Route("/coutries/CreateCollectionCountry")]
        public async Task<IActionResult>CreateCollectionCountry([FromBody] IEnumerable<CountryCreateDTOs> countries)
        {
            var result = await serviceManger.CountriesSerivce.CreateCountryCollection(countries);
            return Ok(result);
        }
    }
}
