﻿using System.ComponentModel.DataAnnotations;

namespace HRM.ViewModels
{
    public class LoginModel
    {
        public string? Email { get; set; }
        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }
}
