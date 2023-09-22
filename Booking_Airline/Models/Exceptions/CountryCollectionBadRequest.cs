namespace Booking_Airline.Models.Exceptions
{
    public class CountryCollectionBadRequest : BadRequestException
    {
        public CountryCollectionBadRequest() : base("Country collection sent from a client is null.") { }
    }
}
