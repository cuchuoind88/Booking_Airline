using Booking_Airline.Models;
using Booking_Airline.Repository.RepositoryBase;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Booking_Airline.Repository.UserRepository
{
    public class UserRepository : RepositoryBase<User> , IUserRepository
    {
        public UserRepository(ApplicationDbContext context ) : base(context){ }

        public void CreateUser(User user) =>
            Create(user);
         
        public async Task<User> GetUserByEmail(string email, bool trackChanges) =>
            await FindByCondition(user => user.Email == email, trackChanges)
            .SingleOrDefaultAsync();
        public async Task<User> GetUserByName(string userName , bool trackChanges) =>
            await FindByCondition(user => user.Username == userName, trackChanges)
            .SingleOrDefaultAsync();

        public async Task<User> GetUserByRefreshToken(string rfToken, bool trackChanges) =>
            await FindByCondition(user => user.refreshTokens.Any(rf => rf.Token == rfToken && rf.IsUsed == false),trackChanges)
            .FirstOrDefaultAsync();


    }
}
