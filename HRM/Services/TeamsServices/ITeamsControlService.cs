using HRM.Models;

namespace HRM.Services.TeamsServices
{
    public interface ITeamsControlService
    {
        public Task<List<Team>> GetListAsync();
        public Task<Team?> GetByIdAsync(int? id);
        public Task AddAsync(Team team);
        public Task DeleteAsync(Team team);
        public Task UpdateAsync(Team team);
        public bool NotEmpty();
        public bool Exists(int id);
    }
}
