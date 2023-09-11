using System.ComponentModel.DataAnnotations;

namespace Booking_Airline.DTOs
{   
    
    public record UserRegisterDTO
    {
        [Required]
        public string Username { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public string Phone { get; set; }
    }
}
