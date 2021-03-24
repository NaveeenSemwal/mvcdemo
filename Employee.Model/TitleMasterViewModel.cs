using Employees.DL.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Employee.Model
{
    public class TitleMasterViewModel
    {
        public int TitleId { get; set; }
        public string Title { get; set; }

        public List<TitleMasterViewModel> TitleListMapping(List<TitleMaster> dbtitleList)
        {
            List<TitleMasterViewModel> titlelist = new List<TitleMasterViewModel>();


            foreach (TitleMaster a in dbtitleList)
            {
                titlelist.Add(new TitleMasterViewModel()
                {
                    Title = a.Title,
                    TitleId = a.TitleId
                });
            }
            return titlelist;
        }
    }
}