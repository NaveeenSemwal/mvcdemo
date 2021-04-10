using Employee.Model;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Employees.DL.Implementation;
namespace Employee.WebAPI.Controllers
{
    public class MasterController : ApiController
    {
        TitleMasterViewModel obj = new TitleMasterViewModel();
        // TODO : Why use this.
        public IEnumerable<TitleMasterViewModel> GetTitleList()
        {
            //TitleRepository titleRepository = new TitleRepository(); // Create this globally.

            //List<TitleMaster> dbtitleList = titleRepository.GetTitles().ToList();

            //List<TitleMasterViewModel> list = obj.TitleListMapping(dbtitleList);

            //return list;
            return null;

        }

        // TODO : Why use this.
        public IEnumerable<CountryMasterViewModel> GetCountryList()
        {
            //CountryRepository country = new CountryRepository(); // Create this globally.

            //List<CountriesMater> dbcounList = country.GetCountries().ToList();

            //List<CountryMasterViewModel> counlist = new List<CountryMasterViewModel>();


            //foreach (CountriesMater a in dbcounList)
            //{
            //    counlist.Add(new CountryMasterViewModel()
            //    {
            //        CountryID = a.CountryID,
            //        CountryName = a.CountryName
            //    });
            //}

            //return counlist;

            return null;
        }
    }
}
