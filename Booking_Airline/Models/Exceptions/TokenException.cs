namespace Booking_Airline.Models.Exceptions
{
    public sealed class TokenException : NotFoundException
    {
        public TokenException(string message) : base(message) { }
    }
}
