using HRM.Models;

namespace HRM.Services.UsersServices
{
    public interface IUsersControleService
    {
        public Task<User?> GetUserByEmailAsync(string name);
        public Task<User?> GetUserByIdAsync(int? id);
        public Task<RoleType?> GetRoleByNameAsync(string name);
        public int GetUserStatusId();
        public Task<List<User>> GetUsersListForCurrentUserAsync(User currentUser);
        public Task AddUserAsync(User user);
        public Task DeleteUserAsync(User user);
        public Task UpdateUserAsync(User user);
        public bool UserExists(int id);
        public bool UsersTableIsNotEmpty();
    }
}
