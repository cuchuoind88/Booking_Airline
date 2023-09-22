namespace Booking_Airline.Models.Exceptions
{
    public class BadRequestException : Exception
    {
        protected BadRequestException(string message)
                                : base(message)
        {
        }
    }
}
