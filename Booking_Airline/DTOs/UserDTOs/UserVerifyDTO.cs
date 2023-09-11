namespace Booking_Airline.DTOs
{
    public record UserVerifyDTO
    {
        public string Email { get; set; }
        public string VerifyToken { get; set; }
    }
}
