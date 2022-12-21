using HRM.Data;
using HRM.Models;
using Microsoft.EntityFrameworkCore;

namespace HRM.Services.TeamsServices
{
    public class TeamsControlService : IGenericControlService<Team>
    {
        private readonly HRMContext _context;
        public TeamsControlService(HRMContext context) => _context = context;

        public async Task<List<Team>> GetListAsync() => await _context.Teams.ToListAsync();
        public async Task<Team?> GetByIdAsync(int? id) => await _context.Teams.FirstOrDefaultAsync(u => u.Id == id);
        public async Task AddAsync(Team team)
        {
            _context.Teams.Add(team);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(Team team)
        {
            _context.Teams.Remove(team);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Team team)
        {
            _context.Teams.Update(team);
            await _context.SaveChangesAsync();
        }

        public bool NotEmpty()
        {
            return (_context.Teams?.Any()).GetValueOrDefault();
        }
        public bool Exists(int id)
        {
            return (_context.Teams?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
