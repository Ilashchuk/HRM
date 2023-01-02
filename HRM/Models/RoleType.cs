namespace HRM.Models
{
    public class RoleType
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public ICollection<User> Users { get; set; } = new List<User>();
    }
}
