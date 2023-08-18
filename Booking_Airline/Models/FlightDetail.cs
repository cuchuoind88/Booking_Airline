namespace Booking_Airline.Models
{
    public class FlightDetail
    {
        public int Id { get; set; }
        public int SourceAirportId { get; set; }
        public Airport SourceAirPort { get; set; }
        public int DestinationAirportId { get; set; }
        public Airport DestinationAirport { get; set; }
        public DateTime DepartureDate { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalDate {get; set; }
        public string  FilghtName { get; set; }
        public string AirlineType { get; set; }

    }
}
