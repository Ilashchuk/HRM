namespace HRM.Models
{
    public class Status
    {
        public int Id { get; set; }
        public int StatusTypeId { get; set; }
        public string? Name { get; set; }

        public StatusType? StatusType { get; set; }
        public ICollection<User> Users { get; set; } = new List<User>();
        public ICollection<Request> Requests { get; set; } = new List<Request>();
    }
}
