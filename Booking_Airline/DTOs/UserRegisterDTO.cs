using System.ComponentModel.DataAnnotations;

namespace Booking_Airline.DTOs
{
    public class UserRegisterDTO
    {
        [Required]
        public String Username { get; set; }
        [Required]
        [EmailAddress]
        public String Email { get; set; }
        [Required]
        public String Password { get; set; }
        public String Phone { get; set; }
    }
}
