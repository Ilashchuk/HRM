using System.ComponentModel.DataAnnotations;

namespace HRM.ViewModels
{
    public class RegisterModel
    {
        public string? Company { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        [DataType(DataType.Password)]
        public string? Password { get; set; }
        [Compare("Password", ErrorMessage = "Password don`t match")]
        [DataType(DataType.Password)]
        [Display(Name = "ConfirmPassword")]
        public string? PasswordConfirm { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; } = DateTime.Now;

    }
}
