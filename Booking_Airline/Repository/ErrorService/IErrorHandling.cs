using Microsoft.AspNetCore.Mvc;

namespace Booking_Airline.Repository.ErrorService
{
    public interface IErrorHandling
    {

        public IActionResult GetBadRequestResult(string message, int statusCode);
    }
}

