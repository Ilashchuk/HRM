namespace HRM.Models
{
    public class Setting
    {
        public int Id { get; set; }
        public int SeekDays { get; set; }
        public int VacationDays { get; set; }
        public override int GetHashCode()
        {
            return Id;
        }
    }
}
