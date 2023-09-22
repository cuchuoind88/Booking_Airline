namespace Booking_Airline.Models
{
    public class Airport
    {   public Guid Id { get; set; }
        public string AirportName { get; set; }
        public Guid CountryId { get; set; }
        public Country Country { get; set; }
        public string AirportCode { get; set; }
        public string AirportCity { get; set; }
        public ICollection<FlightDetail> Flights { get; set; }

    }
}
