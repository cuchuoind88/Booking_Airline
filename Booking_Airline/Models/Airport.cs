namespace Booking_Airline.Models
{
    public class Airport
    {   public int Id { get; set; }
        public string AirportName { get; set; }
        public string AirportCountry { get; set; }
        public string AirportCode { get; set; }
        public string AirportCity { get; set; }
        public ICollection<FlightDetail> Flights { get; set; }
    }
}
