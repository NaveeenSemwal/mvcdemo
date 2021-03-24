using Employee.BL.Interface;
using Employee.Model;
using Employees.DL.Database;
using Employees.DL.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.BL.Implementation
{
    public class TitleService : BaseService, ITitleService
    {
       
        public IEnumerable<TitleMasterViewModel> GetTitles()
        {
            List<TitleMaster> dbtitleList = _titleRepository.GetTitles().ToList();
            TitleMasterViewModel titleMasterobj = new TitleMasterViewModel();
            List<TitleMasterViewModel> titlelist = titleMasterobj.TitleListMapping(dbtitleList);
            return titlelist;
        }
    }
}
