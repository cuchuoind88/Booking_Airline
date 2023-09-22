using Booking_Airline.DTOs.AirportDTOs;
using Booking_Airline.Models;
using Booking_Airline.Repository.RepositoryBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Booking_Airline.Repository.AirportRepository
{
    public class AirportRepository : RepositoryBase<Airport>, IAirportRepository
    {
        public AirportRepository(ApplicationDbContext context) : base(context) { }

        public void CreateAirport(Guid countryId, Airport airport)
        {
            airport.CountryId = countryId;
            Create(airport);
        }

        public Task<IActionResult> DeleteAirport(int AirportId)
        {
            throw new NotImplementedException();
        }

        public async Task<Airport> GetAirport(Guid AirportId, bool track)
        {
            return await FindByCondition(ap => ap.Id.Equals(AirportId), track)
                 .SingleOrDefaultAsync();
        }

        public Task<IEnumerable<Airport>> GetAllAirport()
        {
            throw new NotImplementedException();
        }

        //public Task<IActionResult> GetAllAirport()
        //{

        //}

        public Task<IActionResult> UpdateAirport(int AirportId, Airport updatedAirport)
        {
            throw new NotImplementedException();
        }
    }
}
