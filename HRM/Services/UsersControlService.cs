using HRM.Data;
using HRM.Models;
using Microsoft.EntityFrameworkCore;

namespace HRM.Services
{
    public class UsersControlService : IUsersControleService
    {
        private readonly HRMContext _context;

        public UsersControlService(HRMContext context) => _context = context;

        public User GetUser(string name) => _context.Users.FirstOrDefault(u => u.Email == name)!;
        public User GetUserById(int id) => _context.Users.Include(u => u.Company)
                                                         .Include(u => u.RoleType)
                                                         .Include(u => u.Team)
                                                         .Include(u => u.UserLevel)
                                                         .Include(u => u.Status).FirstOrDefault(u => u.Id == id)!;
        public RoleType GetRole(string name) => _context.RoleTypes.FirstOrDefault(r => r.Name == name)!;
        public int GetUserStatusId() => _context.StatusTypes.First(st => st.Name == "User status").Id;

        public List<User> GetUsersListForCurrentUser(User currentUser)
        {
            RoleType HR = GetRole("HR");
            var  users = _context.Users.Include(u => u.Company).Include(u => u.RoleType)
                    .Include(u => u.Team).Include(u => u.UserLevel).Include(u => u.Status)
                    .Where(u => u.Id != currentUser.Id && u.RoleTypeId != HR.Id);
            if (currentUser.RoleType == HR)
                return users.ToList();
            return users.Where(u => u.TeamId == currentUser.TeamId).ToList();
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
