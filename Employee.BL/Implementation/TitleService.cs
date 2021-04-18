using Employee.BL.Interface;
using Employee.Model;
using Employees.DL.Database;
using Employees.DL.Interface;
using System.Collections.Generic;
using System.Linq;

namespace Employee.BL.Implementation
{
    public class TitleService : BaseService, ITitleService
    {
        public TitleService(IEmployeeRepository employeeRepository, ICountryRepository countryRepository, ITitleMasterRepository titleRepository) : base(employeeRepository, countryRepository, titleRepository)
        {
        }

        public IEnumerable<TitleMasterViewModel> GetTitles()
        {
            List<TitleMaster> dbtitleList = _titleRepository.GetTitles().ToList();
            TitleMasterViewModel titleMasterobj = new TitleMasterViewModel();
            List<TitleMasterViewModel> titlelist = titleMasterobj.TitleListMapping(dbtitleList);
            return titlelist;
        }
    }
}
