using System.ComponentModel.DataAnnotations;

namespace HRM.Models
{
    public class Status
    {
        public int Id { get; set; }
        [Display(Name = "Status Type")]
        public int StatusTypeId { get; set; }
        public string? Name { get; set; }
        [Display(Name = "Status Type")]
        public StatusType? StatusType { get; set; }
        public ICollection<User> Users { get; set; } = new List<User>();
        public ICollection<Request> Requests { get; set; } = new List<Request>();
        public override int GetHashCode()
        {
            return Id;
        }
    }
}
