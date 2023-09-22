using Booking_Airline.DTOs.AirportDTOs;
using Booking_Airline.Models;
using Microsoft.AspNetCore.Mvc;

namespace Booking_Airline.Repository.AirportRepository
{
    public interface IAirportRepository
    {
        public Task<IEnumerable<Airport>>  GetAllAirport();
        public Task<Airport> GetAirport(Guid AirportId,bool track);
        public void  CreateAirport(Guid countryId,Airport airport);
        public Task<IActionResult> UpdateAirport(int AirportId, Airport updatedAirport);
        public Task<IActionResult> DeleteAirport(int AirportId);
    }
}
