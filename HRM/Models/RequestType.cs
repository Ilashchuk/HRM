namespace HRM.Models
{
    public class RequestType
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public ICollection<Request> Requests { get; set; } = new List<Request>();
        public override int GetHashCode()
        {
            return Id;
        }
    }
}
