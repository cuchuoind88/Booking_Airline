using Booking_Airline.DTOs.AirportDTOs;
using Booking_Airline.Models;

namespace Booking_Airline.DTOs.CountryDTOs
{
    public record CountryWithAirportsDTO(Guid Id, string CountryName, string CountryCode, List<AirportDTO> Airports, int NumberOfAirports);
}
