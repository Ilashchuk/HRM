namespace HRM.Models
{
    public class UserDocument
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string? DocumentLinck { get; set; }

        public User? User { get; set; }
    }
}
