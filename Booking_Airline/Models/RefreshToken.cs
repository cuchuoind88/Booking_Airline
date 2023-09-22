namespace Booking_Airline.Models
{
    public class RefreshToken
    {
        public Guid Id { get; set; }
        public string Token { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
       
        public bool IsUsed { get; set; }
    }
}
