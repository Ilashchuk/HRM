using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRM.Models
{
    public class Request
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int RequestTypeId { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }
        public int StatusId { get; set; }

        public User? User { get; set; }
        public RequestType? RequestType { get; set; }
        public Status? Status { get; set; }
        public override int GetHashCode()
        {
            return Id;
        }
    }
}
