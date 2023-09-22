namespace Booking_Airline.DTOs.AirportDTOs
{
    public record AirportCreateDTOs
    {
        public string AirportName { get; set; }
        public string AirportCode { get; set; }
        public string AirportCity { get; set; }
    }
}
