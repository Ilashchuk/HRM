using HRM.Models;
using Microsoft.EntityFrameworkCore;

namespace HRM.Data
{
    public class HRMContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<UserDocument> UserDocuments { get; set; } = null!;
        public DbSet<Request> Requests { get; set; } = null!;
        public DbSet<RequestType> RequestTypes { get; set; } = null!;
        public DbSet<Status> Statuses { get; set; } = null!;
        public DbSet<StatusType> StatusTypes { get; set; } = null!;
        public DbSet<RoleType> RoleTypes { get; set; } = null!;
        public DbSet<UserLevel> UserLevels { get; set; } = null!;
        public DbSet<Team> Teams { get; set; } = null!;
        public DbSet<Company> Companies { get; set; } = null!;
        public DbSet<Setting> Settings { get; set; } = null!;
        public DbSet<OffitialHolliday> OffitialHollidays { get; set; } = null!;

        public HRMContext(DbContextOptions<HRMContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
