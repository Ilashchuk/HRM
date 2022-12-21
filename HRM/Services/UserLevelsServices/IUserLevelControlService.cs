using HRM.Models;

namespace HRM.Services.UserLevelsServices
{
    public interface IUserLevelControlService
    {
        public Task<List<UserLevel>> GetListAsync();
        public Task<UserLevel?> GetByIdAsync(int? id);
        public Task AddAsync(UserLevel userLevel);
        public Task DeleteAsync(UserLevel userLevel);
        public Task UpdateAsync(UserLevel userLevel);
        public bool NotEmpty();
        public bool Exists(int id);
    }
}
