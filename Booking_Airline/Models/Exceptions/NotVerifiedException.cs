namespace Booking_Airline.Models.Exceptions
{
    public class NotVerifiedException : Exception
    {
        public NotVerifiedException(string message) : base(message) { }
    }
}
