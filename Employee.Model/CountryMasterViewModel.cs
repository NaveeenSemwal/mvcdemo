using Employees.DL.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Employee.Model
{
    public class CountryMasterViewModel
    {
        public int CountryID { get; set; }
        public string CountryName { get; set; }

        public List<CountryMasterViewModel> CountryListMapping(List<CountriesMaster> dbcounList)
        {
            List<CountryMasterViewModel> counlist = new List<CountryMasterViewModel>();


            foreach (CountriesMaster a in dbcounList)
            {
                counlist.Add(new CountryMasterViewModel()
                {
                    CountryID = a.Id,
                    CountryName = a.Name
                });
            }

            return counlist;
        }
    }
}