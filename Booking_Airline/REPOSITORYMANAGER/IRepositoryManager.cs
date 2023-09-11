using Booking_Airline.Repository.EmailRepository;
using Booking_Airline.Repository.RefreshTokenRepository;
using Booking_Airline.Repository.RoleRepository;
using Booking_Airline.Repository.UserRepository;
namespace Booking_Airline.REPOSITORYMANAGER
{
    public interface IRepositoryManager
    {
        IUserRepository UserRepository { get; }
        IRefreshTokenRepository RefreshTokenRepository { get; }
        IRoleRepository RoleRepository { get; }
        IEmailRepository emailRepository { get; }
        Task SaveAync();
    }
}
