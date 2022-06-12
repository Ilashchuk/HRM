﻿using System.ComponentModel.DataAnnotations;

namespace HRM.Models
{
    public class User
    {
        public int Id { get; set; }
        [Display(Name = "Full Name")]
        public string? FullName { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }
        [Display(Name = "Status")]
        public int? UserStatusId { get; set; }
        [Display(Name = "Level")]
        public int? UserLevelId { get; set; }
        [Display(Name = "Team")]
        public int? TeamId { get; set; }
        public int? RoleTypeId { get; set; }
        public int CompanyId { get; set; }

        public ICollection<UserDocument> UserDocuments { get; set; } = new List<UserDocument>();
        public ICollection<Request> Requests { get; set; } = new List<Request>();
        public Status? Status { get; set; }
        public RoleType? RoleType { get; set; }
        public UserLevel? UserLevel { get; set; }
        public Team? Team { get; set; }
        public Company? Company { get; set; }
    }
}
