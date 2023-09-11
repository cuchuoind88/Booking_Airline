namespace Booking_Airline.Models.Exceptions
{
    public class UserNotFoundException : NotFoundException
    {
        public UserNotFoundException(string message):base(message) { }
    }
}
