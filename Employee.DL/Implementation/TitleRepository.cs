using Employees.DL.Database;
using Employees.DL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employees.DL.Implementation
{
    public class TitleRepository : ITitleMasterRepository
    {
        public IEnumerable<TitleMaster> GetTitles()
        {
            using (var dbContext = new EMSDemoEntities())
            {
                var titleList = dbContext.TitleMasters.ToList();
                return titleList;
            }
        }
    }
}
