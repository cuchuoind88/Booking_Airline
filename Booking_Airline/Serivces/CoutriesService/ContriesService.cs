using AutoMapper;
using Booking_Airline.DTOs.CountryDTOs;
using Booking_Airline.Models;
using Booking_Airline.Models.Exceptions;
using Booking_Airline.REPOSITORYMANAGER;

namespace Booking_Airline.Serivces.CoutriesService
{
    public class ContriesService : ICountriesSerivce
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryManager repositoryManager;
        public ContriesService ( IRepositoryManager repositoryManager , IMapper mapper )
        {
            _mapper = mapper;
            this.repositoryManager=repositoryManager;
        }
        public async Task< CountyDTOs> CreateCountry(CountryCreateDTOs country)
        {
            var countryEntity = _mapper.Map<Country>(country);
            repositoryManager.countryRepository.CreateCountry(countryEntity);
           await repositoryManager.SaveAync();
            var countryToReturn = _mapper.Map<CountyDTOs>(countryEntity);
            return countryToReturn;
        }

        public async Task< IEnumerable<CountyDTOs>> CreateCountryCollection(IEnumerable<CountryCreateDTOs> countryCollection)
        {
            if (countryCollection is null)
                throw new CountryCollectionBadRequest();
            var countryEntities=_mapper.Map<IEnumerable<Country>>(countryCollection);
            foreach (var countryEntity in countryEntities) {
                repositoryManager.countryRepository.CreateCountry(countryEntity);
            }
           await repositoryManager.SaveAync();
            var countryCollectionToReturn =_mapper.Map<IEnumerable<CountyDTOs>>(countryEntities);
            return countryCollectionToReturn;
        }

        public async Task<IEnumerable<CountryWithAirportsDTO>> GetAllContries()
        {
            return await repositoryManager.countryRepository.GetAllCountryWithAirport();
        }
    }
}
