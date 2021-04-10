using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employees.DL.Database
{
    public class CountriesMaster
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string TwoCharCountryCode { get; set; }
        public string ThreeCharCountryCode { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}
