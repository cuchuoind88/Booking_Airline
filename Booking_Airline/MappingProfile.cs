using AutoMapper;
using Booking_Airline.DTOs.AirportDTOs;
using Booking_Airline.DTOs.CountryDTOs;
using Booking_Airline.Models;

namespace Booking_Airline
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {
            CreateMap<Airport, AirportDTO>();
            CreateMap<Country,CountyDTOs>();
            CreateMap<CountryCreateDTOs, Country>();
            CreateMap<AirportCreateDTOs, Airport>();
        }
    }
}
