namespace Booking_Airline.Models.Exceptions
{
    public class AirportCollectionBadRequest : BadRequestException
    {
        public AirportCollectionBadRequest() : base("Airport collection sent from a client is null.") { }
    }
}
