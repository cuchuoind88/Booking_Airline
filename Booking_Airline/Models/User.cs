namespace Booking_Airline.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string VerifyCode { get; set; }
        public bool IsVerified { get; set; }
        public ICollection<RoleModel> Roles { get; set; }
        public ICollection<RefreshToken> refreshTokens { get; set; }
        public ICollection<TokenRemainLogin> tokenRemainLogins { get; set; }

    }
}
