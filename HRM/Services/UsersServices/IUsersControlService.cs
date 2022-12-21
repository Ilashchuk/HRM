using HRM.Models;

namespace HRM.Services.UsersServices
{
    public interface IUsersControlService : IGenericControlService<User>
    {
        public Task<List<User>> GetUsersListForCurrentUserAsync(User? currentUser);
        public Task<User?> GetUserByEmailAsync(string? name);
    }
}
