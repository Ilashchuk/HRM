using HRM.Models;

namespace HRM.Services
{
    public interface IUsersControleService
    {
        public User GetUser(string name);
        public User GetUserById(int id);
        public RoleType GetRole(string name);
        public int GetUserStatusId();
        public List<User> GetUsersListForCurrentUser(User currentUser);
    }
}
