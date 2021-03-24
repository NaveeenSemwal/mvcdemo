using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Employee.Model
{
    public class ResetPasswordViewModel
    {
        public int EmployeeId { get; set; }
        [Required]
        public string Email { get; set; }
        public string ResetPassword { get; set; }
        public string ConfirmPassword { get; set; }
        public bool IsActive { get; set; }
    }
}