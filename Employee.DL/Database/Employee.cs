using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employees.DL.Database
{
    public class Employee
    {
        public int Id { get; set; }
        public int TitleId { get; set; }

        [Required] // Not Null
        [Column(TypeName = "varchar")]
        [MaxLength(50)] // By default it will MAX
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [Column(TypeName = "varchar")]
        [MaxLength(50)]
        public string Gender { get; set; }
        public Nullable<System.DateTime> Dob { get; set; }
        public string IdProofName { get; set; }
        public int CountryId { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string UserPassword { get; set; }
        public Nullable<bool> IsAdmin { get; set; }

        [ForeignKey("CountryId")]
        public virtual CountriesMaster CountriesMater { get; set; }

        [ForeignKey("TitleId")]
        public virtual TitleMaster TitleMaster { get; set; }

    }
}
