using Booking_Airline.Models;

namespace Booking_Airline.DTOs.AirportDTOs
{
    public record AirportDTO(Guid Id, string AirportName, string AirportCode, string AirportCity);
      
    

}
