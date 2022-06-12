using HRM.Data;
using HRM.Models;
using Microsoft.EntityFrameworkCore;

namespace HRM.Services
{
    public class UsersControlService : IUsersControleService
    {
        private readonly HRMContext _context;

        public UsersControlService(HRMContext context) => _context = context;

        public User GetUser(string name) => _context.Users.FirstOrDefault(u => u.Email == name);
        public RoleType GetRole(string name) => _context.RoleTypes.FirstOrDefault(r => r.Name == name);
        public int GetUserStatusId()
        {
            return _context.StatusTypes.First(st => st.Name == "User status").Id;
        }

        //public List<User> GetUsersList(User currentUser)
        //{
        //    List<User> users = new List<User>();

        //    users = (List<User>)_context.Users.Include(u => u.Company)
        //        .Include(u => u.RoleType).Include(u => u.Team).Include(u => u.UserLevel)
        //        .Where(u => u.Id != currentUser.Id && u.RoleTypeId != GetRole("HR").Id && u.CompanyId == currentUser.CompanyId);

        //    if (currentUser.RoleTypeId == GetRole("TeamLead").Id)
        //    {
        //        users = users.Where(u => u.TeamId == currentUser.TeamId).ToList();
        //    }
        //    return users;
        //}
    }
}
