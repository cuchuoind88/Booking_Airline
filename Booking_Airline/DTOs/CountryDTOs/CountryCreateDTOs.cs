namespace Booking_Airline.DTOs.CountryDTOs
{
    public record CountryCreateDTOs
    {
        public string contryName { get; set; }
        public string countryCode { get; set; }
        public bool Active { get; set; }
    }
}
