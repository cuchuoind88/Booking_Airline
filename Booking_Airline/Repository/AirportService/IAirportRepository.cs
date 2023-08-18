using Booking_Airline.Models;
using Microsoft.AspNetCore.Mvc;

namespace Booking_Airline.Repository.AirportService
{
    public interface IAirportRepository
    {
        public IActionResult GetAllAirport();
        public IActionResult GetAirport(int AirportId);
        public IActionResult CreateAirport(Airport airport);
        public IActionResult UpdateAirport(int AirportId);
        public IActionResult DeleteAirport(int AirportId);
    }
}
