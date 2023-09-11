using System.ComponentModel.DataAnnotations;

namespace Booking_Airline.DTOs.UserDTOs
{   
   
    public record UserLoginDTO
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
