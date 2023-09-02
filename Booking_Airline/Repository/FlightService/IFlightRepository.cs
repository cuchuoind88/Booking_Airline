using Microsoft.AspNetCore.Mvc;

namespace Booking_Airline.Repository.FlightService
{
    public interface IFlightRepository
    {
        public Task<IActionResult> FindFlight();
    }
}
