using HRM.Data;
using HRM.Services.UsersServices;
using HRM.Models;
using Microsoft.EntityFrameworkCore;

namespace HRM.Services.UsersServices
{
    public class UsersControlService : IUsersControlService
    {
        private readonly HRMContext _context;
        public UsersControlService(HRMContext context)
        {
            _context = context;
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
        public async Task<List<User>> GetListAsync() => await _context.Users.ToListAsync();
        public async Task<User?> GetByIdAsync(int? id) => await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        public async Task AddAsync(User t)
        {
            _context.Users.Add(t);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(User t)
        {
            _context.Users.Remove(t);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(User t)
        {
            _context.Users.Update(t);
            await _context.SaveChangesAsync();
        }
        public bool NotEmpty()
        {
            return (_context.Users?.Any()).GetValueOrDefault();
        }
        public bool Exists(int id)
        {
            return (_context.Users?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
