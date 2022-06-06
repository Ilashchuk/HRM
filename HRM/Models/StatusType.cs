namespace HRM.Models
{
    public class StatusType
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public ICollection<Status> Statuses { get; set; } = new List<Status>();
    }
}
