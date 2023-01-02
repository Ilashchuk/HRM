using HRM.Data;
using HRM.Services.UsersServices;
using HRM.Models;
using Microsoft.EntityFrameworkCore;

namespace HRM.Services.UsersServices
{
    public class UsersControlService : GenericControlService<User> ,IUsersControlService
    {
        public UsersControlService(HRMContext context)
            : base(context, context.Users)
        {

        }

        public async Task<User?> GetUserByEmailAsync(string? name) => await _context.Users.FirstOrDefaultAsync(u => u.Email == name)!;
        public async Task<List<User>> GetUsersListForCurrentUserAsync(User? currentUser)
        {
            var HR = await _context.RoleTypes.FirstOrDefaultAsync(r => r.Name == "HR");
            if (HR != null && currentUser != null)
            {
                var users = _context.Users.Include(u => u.Company).Include(u => u.RoleType)
                    .Include(u => u.Team).Include(u => u.UserLevel).Include(u => u.Status)
                    .Where(u => u.Id != currentUser.Id && u.RoleTypeId != HR.Id);
                if (currentUser.RoleType == HR)
                    return users.ToList();
                return users.Where(u => u.TeamId == currentUser.TeamId).ToList();
            }
            return new List<User>();

        }
    }
}
