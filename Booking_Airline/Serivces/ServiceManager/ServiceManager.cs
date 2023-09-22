using AutoMapper;
using Booking_Airline.Models;
using Booking_Airline.REPOSITORYMANAGER;
using Booking_Airline.Serivces.AirportServicesss;
using Booking_Airline.Serivces.AuthenticationService;
using Booking_Airline.Serivces.CoutriesService;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Booking_Airline.Serivces.ServiceManager
{
    public class ServiceManager : IServiceManger
    {
        private readonly Lazy<IAuthenService> authenService;
        private readonly Lazy<ICountriesSerivce> countriesSerivce;
        private readonly Lazy<IAirportService> airportSerivce;
        public ServiceManager (IRepositoryManager repositoryManager, TokenValidationParameters _CheckRefreshToken , IConfiguration _config, IOptions<JWTConfig> options,
            IMapper mapper)
        {
            authenService = new Lazy<IAuthenService>(() => new AuthenService(repositoryManager, _CheckRefreshToken, _config, options));
            countriesSerivce = new Lazy<ICountriesSerivce>(() => new ContriesService(repositoryManager,mapper));
            airportSerivce=new Lazy<IAirportService>(()=> new AirportService(repositoryManager,mapper));    
        }
        
        public IAuthenService AuthenService => authenService.Value;
        public ICountriesSerivce CountriesSerivce => countriesSerivce.Value;
        public IAirportService AirportService => airportSerivce.Value;
    }
}
