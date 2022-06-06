using HRM.Data;
using HRM.Models;

namespace HRM
{
    public class SampleData
    {
        public static void Initialize(HRMContext context)
        {
            if (context.Users.Any())
            {
                return;
            }
            context.RequestTypes.AddRange(
                new RequestType { Name = "Illnes" },
                new RequestType { Name = "Vacations"},
                new RequestType { Name = "Unpaid leave"},
                new RequestType { Name = "Family reasons" }
                );
            context.SaveChanges();
            context.StatusTypes.AddRange(
                new StatusType { Name = "User status" },
                new StatusType { Name = "Request status" }
                );
            context.SaveChanges();
            context.Statuses.AddRange(
                new Status { StatusTypeId = 1, Name = "Working" },
                new Status { StatusTypeId = 1, Name = "On vacation" },
                new Status { StatusTypeId = 2, Name = "Accepted" },
                new Status { StatusTypeId = 2, Name = "Rejected" },
                new Status { StatusTypeId = 2, Name = "Expected" }
                );
            context.SaveChanges();
            context.RoleTypes.AddRange(
                new RoleType { Name = "Developer" },
                new RoleType { Name = "TeamLead" },
                new RoleType { Name = "Mentor" },
                new RoleType { Name = "HR" }
                );
            context.SaveChanges();
            context.UserLevels.AddRange(
                new UserLevel { Name = "Junior" },
                new UserLevel { Name = "Mid" },
                new UserLevel { Name = "Senior" }
                );
            context.SaveChanges();
            context.Companies.AddRange(
                new Company { Name = "Google" },
                new Company { Name = "Apple" }
                );
            context.SaveChanges();
            context.Teams.AddRange(
                new Team { Name = "Some Team" },
                new Team { Name = "Enother Team" }
                );
            context.SaveChanges();
            context.Users.AddRange(
                new User
                {
                    FullName = "Bohdan Ilashchuk",
                    Password = "12345",
                    Email = "44bohdan44@gmail.com",
                    StartDate = new DateTime(2022, 2, 24),
                    UserStatusId = 1,
                    UserLevelId = 3,
                    TeamId = 1,
                    RoleTypeId = 4,
                    CompanyId = 1
                },
                new User
                {
                    FullName = "Mykola Ilashchuk",
                    Password = "1212",
                    Email = "mykola773377@gmail.com",
                    StartDate = new DateTime(2021, 2, 24),
                    UserStatusId = 1,
                    UserLevelId = 2,
                    TeamId = 1,
                    RoleTypeId = 1,
                    CompanyId = 1
                },
                new User
                {
                    FullName = "Oksana Lysko",
                    Password = "54321",
                    Email = "lusko@gmail.com",
                    StartDate = new DateTime(2020, 3, 2),
                    UserStatusId = 1,
                    UserLevelId = 2,
                    TeamId = 1,
                    RoleTypeId = 2,
                    CompanyId = 1
                }
                );
            context.SaveChanges();
            //context.Requests.AddRange(
            //    new Request()
            //    {
            //        UserId = 3,
            //        RequestTypeId = 3,
            //        StartDate = new DateTime(2022, 5, 6),
            //        EndDate = new DateTime(2020, 20, 6),
            //        StatusId = 5
            //    }
            //    );
            context.Settings.AddRange(
                new Setting { SeekDays = 14, VacationDays = 21 }
                );
            context.SaveChanges();
            context.OffitialHollidays.AddRange(
                new OffitialHolliday { Date = new DateTime(2022, 6, 12), Name = "Trinity" },
                new OffitialHolliday { Date = new DateTime(2022, 6, 28), Name = "Day of the Constitution of Ukraine" },
                new OffitialHolliday { Date = new DateTime(2022, 8, 24), Name = "Independence Day of Ukraine" },
                new OffitialHolliday { Date = new DateTime(2022, 10, 14), Name = "Day of Defenders of Ukraine" },
                new OffitialHolliday { Date = new DateTime(2022, 12, 25), Name = "Christmas according to the Gregorian calendar" }
                );
            context.SaveChanges();
        }
    }
}
