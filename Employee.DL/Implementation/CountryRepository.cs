using Employees.DL.Database;
using Employees.DL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employees.DL.Implementation
{
    public class CountryRepository : ICountryRepository
    {
        public IEnumerable<CountriesMater> GetCountries()
        {
            using (var dbContext = new EMSDemoEntities())
            {
                var countryList = dbContext.CountriesMaters.ToList();
                return countryList;
            }
        }
    }
}
