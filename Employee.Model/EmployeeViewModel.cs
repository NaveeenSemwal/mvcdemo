using Employees.DL.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using User = Employees.DL.Database.Employee;

namespace Employee.Model
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
        public string IdProofName { get; set; }
        public string FileName { get; set; }
        public string ImagePath { get; set; }
        [Required]
        public int CountryId { get; set; }
        public string Country { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CreatedOn { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime UpdatedOn { get; set; }
        public bool IsActive { get; set; }
        [Required]
        public string Email { get; set; }
        [RegularExpression(@"^([0-9]{10})$", ErrorMessage = "Invalid Mobile Number.")]
        public string Mobile { get; set; }
        public int PageNumber { get; set; }

        public int PageCount { get; set; }
        public int Total { get; set; }


        public User AddUpdateEmployeeMapping(EmployeeViewModel model)
        {
            User obj = new User();
            obj.EmployeeId = model.EmployeeId;
            obj.TitleId = model.TitleId;
            obj.FirstName = model.FirstName;
            obj.LastName = model.LastName;
            obj.Gender = model.Gender;
            obj.Dob = model.Dob;
            obj.IdProofName = model.IdProofName;
            obj.CountryId = model.CountryId;

            obj.Email = model.Email;
            obj.Mobile = model.Mobile;

            if (model.EmployeeId == 0)
            {
                obj.CreatedOn = DateTime.Now;
                obj.IsActive = false;

            }
            else if (model.EmployeeId > 0)
            {
                obj.IsActive = model.IsActive;
                obj.UpdatedOn = DateTime.Now;

            }

            return obj;
        }

        public static List<EmployeeViewModel> EmployeeListMapping(List<User> emplist)
        {
            List<EmployeeViewModel> employees = new List<EmployeeViewModel>();


            foreach (User a in emplist)
            {
                employees.Add(new EmployeeViewModel()
                {
                    EmployeeId = a.EmployeeId,
                    FirstName = a.TitleMaster.Title + a.FirstName + ' ' + a.LastName,
                    Country = a.CountriesMater.CountryName,
                    Dob = a.Dob.Value,
                    Gender = a.Gender == "M" ? "Male" : "Female",
                    Email = a.Email,
                    Mobile = a.Mobile == null ? "N/A" : a.Mobile,
                    CreatedOn = a.CreatedOn.Value



                });
            }

            return employees;
        }



    }


}