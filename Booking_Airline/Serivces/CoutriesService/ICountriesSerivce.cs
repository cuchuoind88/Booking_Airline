using Booking_Airline.DTOs.CountryDTOs;

namespace Booking_Airline.Serivces.CoutriesService
{
    public interface ICountriesSerivce
    {
        public Task<IEnumerable<CountryWithAirportsDTO>> GetAllContries();
        public Task<IEnumerable<CountyDTOs>> CreateCountryCollection
(IEnumerable<CountryCreateDTOs> companyCollection);
        public Task <CountyDTOs> CreateCountry(CountryCreateDTOs country);
    }
}
