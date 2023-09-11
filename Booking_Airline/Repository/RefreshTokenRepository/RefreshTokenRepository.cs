using Booking_Airline.Models;
using Booking_Airline.Repository.RepositoryBase;
using Booking_Airline.Repository.UserRepository;
using Microsoft.EntityFrameworkCore;

namespace Booking_Airline.Repository.RefreshTokenRepository
{
    public class RefreshTokenRepository : RepositoryBase<RefreshToken>, IRefreshTokenRepository
    {   public RefreshTokenRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext) { }

        public async Task<RefreshToken> GetTokenByRefreshToken(string refreshToken, bool TrackChanges)
        {
            return await FindByCondition(rf => rf.Token == refreshToken, TrackChanges)
                    .FirstOrDefaultAsync();

        }

        public async Task<IEnumerable<RefreshToken>> GetTokenByUserID(string userId,bool TrackChanges)
        {
            return await FindByCondition(t => t.UserId == int.Parse(userId), TrackChanges)
                .ToListAsync();
        }
        public void CreateToken(RefreshToken token) => Create(token);
    }
}
