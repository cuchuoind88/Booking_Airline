using Booking_Airline.Models;
using Microsoft.AspNetCore.Mvc;

namespace Booking_Airline.Repository.AirportService
{
    public interface IAirportRepository
    {
        public  Task<IActionResult> GetAllAirport();
        public Task <IActionResult> GetAirport(int AirportId);
        public Task <IActionResult> CreateAirport(Airport airport);
        public Task <IActionResult> UpdateAirport(int AirportId);
        public Task <IActionResult> DeleteAirport(int AirportId);
    }
}
