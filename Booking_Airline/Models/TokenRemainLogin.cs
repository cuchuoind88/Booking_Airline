namespace Booking_Airline.Models
{
    public class TokenRemainLogin
    {
        public Guid Id { get; set; }
        public string TokenId { get; set; }
        //Navigate property
        public Guid UserId { get;set; }
        public User User { get; set; }
        public bool IsExpired { get; set; }
    }
}
