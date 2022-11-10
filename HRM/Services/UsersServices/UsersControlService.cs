using HRM.Data;
using HRM.Services.UsersServices;
using HRM.Models;
using Microsoft.EntityFrameworkCore;

namespace HRM.Services.UsersServices
{
    public class UsersControlService : IUsersControlService
    {
        private readonly HRMContext _context;

        public UsersControlService(HRMContext context) => _context = context;

        public async Task<User?> GetUserByEmailAsync(string? name) => await _context.Users.FirstOrDefaultAsync(u => u.Email == name)!;
        public async Task<User?> GetUserByIdAsync(int? id) => await _context.Users.Include(u => u.Company)
                                                         .Include(u => u.RoleType)
                                                         .Include(u => u.Team)
                                                         .Include(u => u.UserLevel)
                                                         .Include(u => u.Status).FirstOrDefaultAsync(u => u.Id == id);
        //public async Task<RoleType?> GetRoleByNameAsync(string name) => await _context.RoleTypes.FirstOrDefaultAsync(r => r.Name == name)!;
        //public int GetUserStatusId() => _context.StatusTypes.First(st => st.Name == "User status").Id;

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
        public async Task AddUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(User user)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateUserAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }
        public bool UserExists(int id)
        {
            return (_context.Users?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        public bool UsersTableIsNotEmpty()
        {
            return (_context.Users?.Any()).GetValueOrDefault();
        }

    }
}
