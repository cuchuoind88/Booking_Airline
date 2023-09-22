using Booking_Airline.DTOs.AirportDTOs;
using Booking_Airline.DTOs.CountryDTOs;

namespace Booking_Airline.Serivces.AirportServicesss
{
    public interface IAirportService
    {
        public  Task<AirportDTO> CreateAirport(Guid countryId , AirportCreateDTOs airportCreateDTOs);
        public Task <IEnumerable<AirportDTO>> CreateAirportCollection(Guid countryId, IEnumerable<AirportCreateDTOs> companyCollection);
    }
}
