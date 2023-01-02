using HRM.Data;
using Microsoft.EntityFrameworkCore;

namespace HRM.Services
{
    public class GenericControlService<T> : IGenericControlService<T> where T : class
    {
        public readonly HRMContext _context;
        private readonly DbSet<T> _table;
        public GenericControlService(HRMContext context, DbSet<T> table)
        {
            _table = table;
            _context = context;
        }

        public async Task<List<T>> GetListAsync() => await _table.ToListAsync();
        public async Task<T?> GetByIdAsync(int? id) => await _table.FirstOrDefaultAsync(u => u.GetHashCode() == id);
        public async Task AddAsync(T t)
        {
            _table.Add(t);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(T t)
        {
            _table.Remove(t);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(T t)
        {
            _table.Update(t);
            await _context.SaveChangesAsync();
        }
        public bool NotEmpty()
        {
            return (_table?.Any()).GetValueOrDefault();
        }
        public bool Exists(int id)
        {
            return (_table?.Any(e => e.GetHashCode() == id)).GetValueOrDefault();
        }
    }
}
