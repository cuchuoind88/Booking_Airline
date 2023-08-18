using System.ComponentModel.DataAnnotations;

namespace Booking_Airline.DTOs
{
    public class UserLoginDTO
    {
        [Required]
        public String Username { get; set; }

        [Required]
        public String Password { get; set; }
    }
}
