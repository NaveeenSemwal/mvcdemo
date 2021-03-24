using Employee.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.BL.Interface
{
    public interface ICountryService
    {
        IEnumerable<CountryMasterViewModel> GetCountries();
    }
}
