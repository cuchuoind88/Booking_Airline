using Booking_Airline.Models;

namespace Booking_Airline.Repository.RoleRepository
{
    public interface IRoleRepository
    {
        public Task<List<string>> GetRoleModelsByUserId(Guid userId, bool tracking);
        public Task<RoleModel> GetRole(string role, bool tracking);
    }
}
