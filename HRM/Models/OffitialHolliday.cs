using System.ComponentModel.DataAnnotations;

namespace HRM.Models
{
    public class OffitialHolliday
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
    }
}
