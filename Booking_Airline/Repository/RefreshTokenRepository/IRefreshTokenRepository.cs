using Booking_Airline.Models;

namespace Booking_Airline.Repository.RefreshTokenRepository
{
    public interface IRefreshTokenRepository
    {
        public Task<IEnumerable<RefreshToken>> GetTokenByUserID(Guid userId , bool TrackChanges);
        public Task<RefreshToken> GetTokenByRefreshToken(string refreshToken , bool TrackChanges);
        public void CreateToken(RefreshToken token);
    }
}
