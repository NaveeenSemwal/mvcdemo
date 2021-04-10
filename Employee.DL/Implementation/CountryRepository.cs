using Employees.DL.Database;
using Employees.DL.Interface;
using System.Collections.Generic;
using System.Linq;

namespace Employees.DL.Implementation
{
    public class CountryRepository : ICountryRepository
    {
        public IEnumerable<CountriesMaster> GetCountries()
        {
            using (var dbContext = new EMSDemoEntities())
            {
                var countryList = dbContext.CountriesMasters.ToList();
                return countryList;
            }
        }
    }
}
