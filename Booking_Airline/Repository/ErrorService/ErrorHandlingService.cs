using Booking_Airline.Models;
using Microsoft.AspNetCore.Mvc;

namespace Booking_Airline.Repository.ErrorService
{
    public class ErrorHandlingService : IErrorHandling
    {
        public IActionResult GetBadRequestResult(string message, int statusCode )
        {
            var error = new ErrorModel
            {
                StatusCode = statusCode,
                Message = message
            };
            return new BadRequestObjectResult(error);
        }
    }
}
