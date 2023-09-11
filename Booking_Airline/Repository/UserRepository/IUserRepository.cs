using Booking_Airline.Models;

namespace Booking_Airline.Repository.UserRepository
{
    public interface IUserRepository
    {
        public  Task<User> GetUserByName(string userName, bool trackChanges);
        public Task<User> GetUserByRefreshToken(string rfToken,bool trackChanges);
        public Task<User> GetUserByEmail(string email, bool trackChanges);
        public void CreateUser( User user );
    }
}
