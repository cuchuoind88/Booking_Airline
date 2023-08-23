namespace Booking_Airline.Models
{
    public class RefreshToken
    {
        public int Id { get; set; }
        public string Token { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
       
        public bool IsUsed { get; set; }
    }
}
