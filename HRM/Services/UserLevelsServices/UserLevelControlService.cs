using HRM.Data;
using HRM.Models;
using Microsoft.EntityFrameworkCore;

namespace HRM.Services.UserLevelsServices
{
    public class UserLevelControlService : IUserLevelControlService
    {
        private readonly HRMContext _context;
        public UserLevelControlService(HRMContext context) => _context = context;

        public async Task<List<UserLevel>> GetListAsync() => await _context.UserLevels.ToListAsync();
        public async Task<UserLevel?> GetByIdAsync(int? id) => await _context.UserLevels.FirstOrDefaultAsync(u => u.Id == id);
        public async Task AddAsync(UserLevel userLevel)
        {
            _context.UserLevels.Add(userLevel);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(UserLevel userLevel)
        {
            _context.UserLevels.Remove(userLevel);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(UserLevel userLevel)
        {
            _context.UserLevels.Update(userLevel);
            await _context.SaveChangesAsync();
        }

        public bool NotEmpty()
        {
            return (_context.UserLevels?.Any()).GetValueOrDefault();
        }
        public bool Exists(int id)
        {
            return (_context.UserLevels?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
