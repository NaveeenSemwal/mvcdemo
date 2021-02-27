using MVCDemoEFr.EFModels;
using MVCDemoEFr.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCDemoEFr.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.CountryList = new SelectList(GetCountryList(), "CountryId", "CountryName","0");
            ViewBag.TitleList = new SelectList(GetTitleList(), "TitleId", "Title","0");
            return View();
        }

        [HttpPost]
        public ActionResult Create(EmployeeViewModel model)
        {
            var fileName = string.Empty;

            if (model.IdProofName != null)
            {

                if (model.IdProofName.ContentLength > 0)
                {
                    fileName = Path.GetFileName(model.IdProofName.FileName);


                }
            }

            tblEmployee obj = new tblEmployee();
            MVCDemoEntities dbContext = new MVCDemoEntities();
            if (model.EmployeeId == 0)
            {
                obj.TitleId = model.TitleId;
                obj.FirstName = model.FirstName;
                obj.LastName = model.LastName;
                obj.Gender = model.Gender;
                obj.Dob = model.Dob;
                obj.IdProofName = fileName;
                obj.CountryId = model.CountryId;
                obj.IsActive = model.IsActive;
                obj.CreatedOn = DateTime.Now;
                dbContext.tblEmployees.Add(obj);
                dbContext.SaveChanges();
                int employeeId = obj.EmployeeId;
                if (employeeId > 0)
                {
                    var folderName = "EMP-" + employeeId;
                    string uploadRoot = Server.MapPath("~/Images/");
                    string folder = string.Format(uploadRoot + "/{0}/", folderName);
                    if (!Directory.Exists(folder))
                    {
                        Directory.CreateDirectory(folder);
                        if (fileName != null && fileName != "")
                        {
                            var path = Path.Combine(folder, fileName);
                            model.IdProofName.SaveAs(path);
                        }
                    }

                }

            }
            return View();
        }

        [HttpGet]
        public IEnumerable<tblTitleMaster> GetTitleList()
        {
            using (var dbContext = new MVCDemoEntities())
            {
                var titleList = dbContext.tblTitleMasters.ToList();
                tblTitleMaster tblTitleMaster = new tblTitleMaster();
                tblTitleMaster.TitleId = 0;
                tblTitleMaster.Title = "Select title";
                titleList.Add(tblTitleMaster);
                return titleList;
            }
        }

        [HttpGet]
        public IEnumerable<tblCountriesMater> GetCountryList()
        {
            using (var dbContext = new MVCDemoEntities())
            {
                var countryList = dbContext.tblCountriesMaters.ToList();


                tblCountriesMater tblCountriesMater = new tblCountriesMater();
                tblCountriesMater.CountryID = 0;
                tblCountriesMater.CountryName = "Select the country";
                countryList.Add(tblCountriesMater);


                return countryList;
            }
        }


    }
}
