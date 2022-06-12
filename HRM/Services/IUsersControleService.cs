using HRM.Models;

namespace HRM.Services
{
    public interface IUsersControleService
    {
        public User GetUser(string name);
        public RoleType GetRole(string name);
        public int GetUserStatusId();
        //public List<User> GetUsersList(User currentUser);
    }
}
