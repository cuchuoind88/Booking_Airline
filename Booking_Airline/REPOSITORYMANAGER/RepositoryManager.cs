using Booking_Airline.Models;
using Booking_Airline.Repository.EmailRepository;
using Booking_Airline.Repository.RefreshTokenRepository;
using Booking_Airline.Repository.RoleRepository;
using Booking_Airline.Repository.UserRepository;
using Microsoft.Extensions.Options;

namespace Booking_Airline.REPOSITORYMANAGER
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly ApplicationDbContext _context;
        private readonly Lazy<IUserRepository> _userRepository;
        private readonly Lazy<IRefreshTokenRepository> _refreshTokenRepository;
        private readonly Lazy<IRoleRepository> _roleRepository;
        private readonly IConfiguration _config;
        //protected readonly IOptions<JWTConfig> options;
        private readonly Lazy<IEmailRepository> _mailRepository;
        public RepositoryManager ( ApplicationDbContext applicationDbContext , IConfiguration config)
        {
            _context = applicationDbContext;
            _config = config;
            _userRepository=new Lazy<IUserRepository> (() => new UserRepository(_context));
            _refreshTokenRepository = new Lazy<IRefreshTokenRepository>(() => new RefreshTokenRepository(_context));
            _roleRepository = new Lazy<IRoleRepository>(() => new RoleRepository(_context));
            _mailRepository= new Lazy<IEmailRepository>(() => new EmailRepository( _config));
        }
        public IUserRepository UserRepository => _userRepository.Value;

        public IRefreshTokenRepository RefreshTokenRepository => _refreshTokenRepository.Value;

        public IRoleRepository RoleRepository =>_roleRepository.Value;

        public IEmailRepository emailRepository => _mailRepository.Value;

        public async Task SaveAync()
        {
           await _context.SaveChangesAsync();
        }
    }
}
