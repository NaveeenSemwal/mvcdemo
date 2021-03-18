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
        public string Title { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Dob { get; set; }
        [Required]
        public HttpPostedFileBase IdProofName { get; set; }
        public string FileName { get; set; }
        public string ImagePath { get; set; }
        [Required]
        public int CountryId { get; set; }
        public string Country { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CreatedOn { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime UpdatedOn { get; set; }
        [Required]
        public bool IsActive { get; set; }
        [Required]
        public string Email { get; set; }
        [RegularExpression(@"^([0-9]{10})$", ErrorMessage = "Invalid Mobile Number.")]
        public string Mobile { get; set; }
        public int PageNumber { get; set; }

        public int PageCount { get; set; }
        public int Total { get; set; }
    }


}