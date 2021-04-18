using Employee.BL.Interface;
using Employee.Model;
using Employees.DL.Database;
using Employees.DL.Interface;
using System.Collections.Generic;
using System.Linq;

namespace Employee.BL.Implementation
{
    public class CountryService : BaseService, ICountryService
    {
        public CountryService(IEmployeeRepository employeeRepository, ICountryRepository countryRepository, ITitleMasterRepository titleRepository) : base(employeeRepository, countryRepository, titleRepository)
        {
        }

        public IEnumerable<CountryMasterViewModel> GetCountries()
        {
            List<CountriesMaster> dbcounList = _countryRepository.GetCountries().ToList();
            CountryMasterViewModel objCoun = new CountryMasterViewModel();
            List<CountryMasterViewModel> counlist = objCoun.CountryListMapping(dbcounList);
            return counlist;
        }
    }
}
