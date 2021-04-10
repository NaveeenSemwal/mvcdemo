using Employee.BL.Interface;
using Employee.Model;
using Employees.DL.Database;
using Employees.DL.Implementation;
using Employees.DL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.BL.Implementation
{
    public class CountryService : BaseService, ICountryService
    {

        public IEnumerable<CountryMasterViewModel> GetCountries()
        {
            List<CountriesMaster> dbcounList = _countryRepository.GetCountries().ToList();
            CountryMasterViewModel objCoun = new CountryMasterViewModel();
            List<CountryMasterViewModel> counlist = objCoun.CountryListMapping(dbcounList);
            return counlist;
        }
    }
}
