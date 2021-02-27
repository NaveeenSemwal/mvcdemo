using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCDemoEFr.ViewModels
{
    public class EmployeeViewModel
    {
        public int EmployeeId { get; set; }
        public int TitleId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public DateTime Dob { get; set; }
        public HttpPostedFileBase IdProofName { get; set; }
        public int CountryId { get; set; }
        public bool IsActive { get; set; }
    }
}