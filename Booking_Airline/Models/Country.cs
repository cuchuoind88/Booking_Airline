namespace Booking_Airline.Models
{
    public class Country
    {
        public int Id { get; set; }
        public string contryName { get; set; }
        public string countryCode { get; set; }
        public bool Active { get; set; }
        public ICollection<Airport> Airports { get; set; }
    }
}
