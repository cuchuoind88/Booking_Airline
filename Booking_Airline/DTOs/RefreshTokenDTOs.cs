using Booking_Airline.Models;

namespace Booking_Airline.DTOs
{
    public record RefreshTokenDTOs
    {
        public string Token { get; set; }
        public int UserId { get; set; }
        public bool IsUsed { get; set; }
    } 
}
