using Booking_Airline.Models;
using Booking_Airline.Repository.RepositoryBase;
using Microsoft.EntityFrameworkCore;

namespace Booking_Airline.Repository.RoleRepository
{
    public class RoleRepository : RepositoryBase<RoleModel>, IRoleRepository
    {
        public RoleRepository(ApplicationDbContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task<RoleModel> GetRole(string role, bool tracking) =>
            await FindByCondition(r => r.RoleName == role, tracking).FirstOrDefaultAsync();


        public async Task<List<string>> GetRoleModelsByUserId(Guid userId, bool tracking) =>
            await FindByCondition(role => role.Users.Any(user => user.Id.Equals(userId)), tracking).Select(role => role.RoleName)
            .ToListAsync();
          
        
    }
}
