namespace Booking_Airline.DTOs.CountryDTOs
{
    public record CountyDTOs
    {
        public Guid Id { get; set; }
        public string contryName { get; set; }
        public string countryCode { get; set; }
        public bool Active { get; set; }
    }
}
