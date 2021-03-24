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

        public List<CountryMasterViewModel> CountryListMapping(List<CountriesMater> dbcounList)
        {
            List<CountryMasterViewModel> counlist = new List<CountryMasterViewModel>();


            foreach (CountriesMater a in dbcounList)
            {
                counlist.Add(new CountryMasterViewModel()
                {
                    CountryID = a.CountryID,
                    CountryName = a.CountryName
                });
            }

            return counlist;
        }
    }
}