using Booking_Airline.Models;
using Booking_Airline.REPOSITORYMANAGER;
using Booking_Airline.Serivces.AuthenticationService;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Booking_Airline.Serivces.ServiceManager
{
    public class ServiceManager : IServiceManger
    {
        private readonly Lazy<IAuthenService> authenService;
        public ServiceManager (IRepositoryManager repositoryManager, TokenValidationParameters _CheckRefreshToken , IConfiguration _config, IOptions<JWTConfig> options)
        {
            authenService = new Lazy<IAuthenService>(() => new AuthenService(repositoryManager, _CheckRefreshToken, _config, options));
        }
        
        public IAuthenService AuthenService => authenService.Value;
    }
}
