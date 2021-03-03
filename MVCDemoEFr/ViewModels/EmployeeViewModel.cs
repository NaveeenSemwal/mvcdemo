using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCDemoEFr.ViewModels
{
    public class EmployeeViewModel
    {
        public int EmployeeId { get; set; }
        [Required]
        public int TitleId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public DateTime Dob { get; set; }
        [Required]
        public HttpPostedFileBase IdProofName { get; set; }
        [Required]
        public int CountryId { get; set; }
        [Required]
        public bool IsActive { get; set; }
    }
}