using AutoMapper;
using Booking_Airline.DTOs.AirportDTOs;
using Booking_Airline.DTOs.CountryDTOs;
using Booking_Airline.Models;
using Booking_Airline.Models.Exceptions;
using Booking_Airline.REPOSITORYMANAGER;

namespace Booking_Airline.Serivces.AirportServicesss
{ 
    public class AirportService : IAirportService
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryManager repositoryManager;
        public AirportService( IRepositoryManager repositoryManager, IMapper mapper)
        {
            _mapper = mapper;
            this.repositoryManager=repositoryManager;
        }
        public async Task< AirportDTO> CreateAirport(Guid countryId, AirportCreateDTOs airportCreateDTOs)
        {
            var AirportEntity = _mapper.Map<Airport>(airportCreateDTOs);
            repositoryManager.airportRepository.CreateAirport(countryId, AirportEntity);
            await repositoryManager.SaveAync();
            var AirportToReturn = _mapper.Map<AirportDTO>(AirportEntity);
            return AirportToReturn;
        }

        public async Task< IEnumerable<AirportDTO>> CreateAirportCollection(Guid countryId ,IEnumerable<AirportCreateDTOs> airportCollection)
        {
            if (airportCollection is null)
                throw new AirportCollectionBadRequest();
            var airportEntities = _mapper.Map<IEnumerable<Airport>>(airportCollection);
            foreach (var airportEntity in airportEntities)
            {
                airportEntity.CountryId = countryId;
                repositoryManager.airportRepository.CreateAirport(countryId ,airportEntity);
            }
            await repositoryManager.SaveAync();
            var airportCollectionToReturn = _mapper.Map<IEnumerable<AirportDTO>>(airportEntities);
            return airportCollectionToReturn;
        }
    }
}
