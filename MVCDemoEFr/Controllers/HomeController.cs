using Employee.DL.Implementation;
using Employees.DL.Database;
using Employees.DL.Implementation;
using Employees.DL.Interface;
using MVCDemoEFr.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using User = Employees.DL.Database.Employee;

namespace MVCDemoEFr.Controllers
{
    public class HomeController : Controller
    {
        readonly IEmployeeRepository _employeeRepository;

        public HomeController()
        {
            _employeeRepository = new EmployeeRepository();
        }

        public ActionResult Index()
        {
            var emplist = _employeeRepository.GetList();
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
            ViewBag.CountryList = new SelectList(GetCountryList(), "CountryId", "CountryName");
            ViewBag.TitleList = new SelectList(GetTitleList(), "TitleId", "Title");
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

            // TODO :

            // 1. Create Global object of EmployeeRepository using Interface using Constructor.
            // 2. Return to Home controller if Insert is successful only.

            User obj = new User();
            obj.EmployeeId = model.EmployeeId;
            obj.TitleId = model.TitleId;
            obj.FirstName = model.FirstName;
            obj.LastName = model.LastName;
            obj.Gender = model.Gender;
            obj.Dob = model.Dob;
            obj.IdProofName = fileName;
            obj.CountryId = model.CountryId;
            obj.IsActive = model.IsActive;
            obj.CreatedOn = DateTime.Now;

            _employeeRepository.Add(obj);
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
                return RedirectToAction("Index");
            }
            return View();

        }

        [HttpGet] // TODO : Why use this.
        public IEnumerable<TitleMasterViewModel> GetTitleList()
        {
            TitleRepository titleRepository = new TitleRepository(); // Create this globally.

            List<TitleMaster> dbtitleList = titleRepository.GetTitles().ToList();

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

        [HttpGet] // TODO : Why use this.
        public IEnumerable<CountryMasterViewModel> GetCountryList()
        {
            CountryRepository country = new CountryRepository(); // Create this globally.

            List<CountriesMater> dbcounList = country.GetCountries().ToList();

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
