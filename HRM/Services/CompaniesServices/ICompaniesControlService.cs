using HRM.Models;

namespace HRM.Services.CompaniesServices
{
    public interface ICompaniesControlService
    {
        public Task<List<Company>> GetListAsync();
        public Task<Company?> GetByIdAsync(int? id);
        public Task AddAsync(Company company);
        public Task DeleteAsync(Company company);
        public Task UpdateAsync(Company company);
        public bool NotEmpty();
        public bool Exists(int id);
    }
}
