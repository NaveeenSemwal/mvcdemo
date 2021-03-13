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
using PagedList;
using User = Employees.DL.Database.Employee;
using System.Configuration;
using System.Web;

namespace MVCDemoEFr.Controllers
{
    public class HomeController : Controller
    {
        readonly IEmployeeRepository _employeeRepository;

        public HomeController()
        {
            _employeeRepository = new EmployeeRepository();
        }

        /// <summary>
        /// https://docs.microsoft.com/en-us/aspnet/mvc/overview/older-versions/getting-started-with-ef-5-using-mvc-4/sorting-filtering-and-paging-with-the-entity-framework-in-an-asp-net-mvc-application
        /// </summary>
        /// <returns></returns>
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page = 1)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "FirstName_asc" : "FirstName_desc";
            ViewBag.DateSortParm = sortOrder == "CreatedDate" ? "CreatedDate_asc" : "CreatedDate_desc";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            List<User> emplist = _employeeRepository.GetList(searchString).ToList();
            emplist = SortEmployees(sortOrder, emplist);

            List<EmployeeViewModel> employees = PopulateEmployees(emplist);
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(employees.ToPagedList(pageNumber, pageSize));
        }

        private static List<User> SortEmployees(string sortOrder, List<User> emplist)
        {
            switch (sortOrder)
            {
                case "FirstName_desc":
                    emplist = emplist.OrderByDescending(s => s.FirstName).ToList();
                    break;
                case "UpdatedOn":
                    emplist = emplist.OrderByDescending(s => s.UpdatedOn).ToList();
                    break;
                case "CreatedDate_desc":
                    emplist = emplist.OrderByDescending(s => s.CreatedOn).ToList();
                    break;
                default:
                    emplist = emplist.OrderBy(s => s.FirstName).ToList();
                    break;
            }

            return emplist;
        }

        private static List<EmployeeViewModel> PopulateEmployees(List<User> emplist)
        {
            List<EmployeeViewModel> employees = new List<EmployeeViewModel>();


            foreach (User a in emplist)
            {
                employees.Add(new EmployeeViewModel()
                {
                    EmployeeId = a.EmployeeId,
                    FirstName = a.FirstName,
                    Dob = a.Dob.Value,
                    CreatedOn = a.CreatedOn.Value

                });
            }

            return employees;
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

            User obj = PopulateEmployeeDTO(model, fileName);

            int employeeId = obj.EmployeeId;
            if (employeeId > 0)
            {
                Save(model, fileName, employeeId);
                return RedirectToAction("Index");
            }
            return View();

        }

        private void Save(EmployeeViewModel model, string fileName, int employeeId)
        {
            var folderName = "EMP-" + employeeId;
            string uploadRoot = Server.MapPath("~/Images/");
            string folder = string.Format(uploadRoot + "/{0}/", folderName);
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);

            }
            if (fileName != null && fileName != "")
            {
                var path = Path.Combine(folder, fileName);
                model.IdProofName.SaveAs(path);
            }
        }

        private User PopulateEmployeeDTO(EmployeeViewModel model, string fileName)
        {
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
            if (model.EmployeeId == 0)
            {
                obj.CreatedOn = DateTime.Now;

                _employeeRepository.Add(obj);
            }
            else if (model.EmployeeId > 0)
            {
                obj.UpdatedOn = DateTime.Now;
                _employeeRepository.Update(obj);
            }

            return obj;
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


        [HttpGet]
        public ActionResult Edit(int id)
        {

            ViewBag.IdProofShow = false;
            ViewBag.ImageShow = true;

            ViewBag.CountryList = new SelectList(GetCountryList(), "CountryId", "CountryName");
            ViewBag.TitleList = new SelectList(GetTitleList(), "TitleId", "Title");
            var employee = _employeeRepository.GetEmpById(id);

            EmployeeViewModel viewModel = new EmployeeViewModel();


            viewModel.EmployeeId = employee.EmployeeId;
            viewModel.TitleId = employee.TitleId.Value;
            viewModel.FirstName = employee.FirstName;
            viewModel.LastName = employee.LastName;
            viewModel.Gender = employee.Gender;
            viewModel.Dob = employee.Dob.Value;
            viewModel.FileName = employee.IdProofName;
            viewModel.CountryId = employee.CountryId.Value;
            viewModel.IsActive = employee.IsActive.Value;
            viewModel.ImagePath = "E:/KIRAN/MVCDemo/MVCDemoEFr/Images/EMP-" + employee.EmployeeId + "/" + employee.IdProofName;
            viewModel.FileName = employee.IdProofName;
            ViewBag.EmployeeId = employee.EmployeeId;
            ViewBag.ImagePath = "https://localhost:44349/Images/EMP-" + employee.EmployeeId + "/" + employee.IdProofName;
            ViewBag.ImageName = employee.IdProofName;


            return View(viewModel);
        }

        [HttpPost]
        public JsonResult DeleteImage(int id, string filename)
        {
            bool isDelete = false;
            var path = Path.Combine(Server.MapPath("~/Images/EMP-") + id + "/" + filename);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
                 isDelete = true;
            }
            return Json(isDelete, JsonRequestBehavior.AllowGet);
        }

    }

}
