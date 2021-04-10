using Employees.DL.Database;
using System.Collections.Generic;

namespace Employees.DL.Interface
{
    public interface ICountryRepository
    {
        IEnumerable<CountriesMaster> GetCountries();
    }
}
