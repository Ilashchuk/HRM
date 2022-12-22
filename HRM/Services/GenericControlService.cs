using HRM.Data;
using HRM.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace HRM.Services
{
    public class GenericControlService<T> : IGenericControlService<T>
    {
        private readonly HRMContext _context;
        public GenericControlService(HRMContext context) => _context = context;

        public DbSet<T> GetTable()
        {
            Type genericType = typeof(T);
            Type contextType = typeof(HRMContext);
            var prop = contextType.GetProperties().FirstOrDefault(p => p.PropertyType.Name == genericType.Name);
            return (DbSet<T>)prop;
        }
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
