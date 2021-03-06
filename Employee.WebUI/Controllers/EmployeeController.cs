﻿using Employee.BL.Interface;
using Employee.Model;
using Employee.Utilities;
using PagedList;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using User = Employees.DL.Database.Employee;

namespace Employee.WebUI.Controllers
{
    [RoutePrefix("ems")]
    // [Authorize]
    public class EmployeeController : BaseController
    {
        public EmployeeController(IEmployeeService employeeService, ICountryService countryService, ITitleService titleService) : base(employeeService, countryService, titleService)
        {
        }

        [Route("employees")]
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page = 1)
        {
            int pageSize = 3;

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


            int pageNumber = (page ?? 1);
            return View(_employeeService.GetList(searchString, sortOrder, page, pageSize).ToList().ToPagedList(pageNumber, pageSize));
        }

      
        [Route("employees/{id:int}")]
        //[Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult Edit(int id)
        {

            //ViewBag.CountryList = new SelectList(GetCountryList(), "CountryId", "CountryName");
            //ViewBag.TitleList = new SelectList(GetTitleList(), "TitleId", "Title");
            EmployeeViewModel employee = _employeeService.GetEmpById(id);
            ViewBag.EmployeeId = employee.EmployeeId;
            var url = ConfigurationManager.AppSettings["Baseurl"];
            ViewBag.ImagePath = url + "Images/EMP-" + employee.EmployeeId + "/" + employee.IdProofName;
            ViewBag.ImageName = employee.IdProofName;


            return View(employee);
        }

        [Route("employee/delete/file/{id:int}")]
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

        
        [Route("employee/register")]
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult> Create()
        {
            // Implement Capta code

            ViewBag.CountryList = new SelectList(await GetCountryList(), "CountryId", "CountryName");
            ViewBag.TitleList = new SelectList( await GetTitleList(), "TitleId", "Title");
            return View();
        }



        [HttpGet]
        public async Task<IEnumerable<TitleMasterViewModel>> GetTitleList()
        {
            return await Task.FromResult(_titleService.GetTitles().ToList());
        }

        [HttpGet]
        public async Task<IEnumerable<CountryMasterViewModel>> GetCountryList()
        {
            return await Task.FromResult(_countryService.GetCountries().ToList());

        }

        // ems/employees/register
        [Route("employee/register")]
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EmployeeViewModel model)
        {
            var files = Request.Files;
            if (files.Count > 0)
            {

                var postedFile = Request.Files[0];
                model.IdProofName = Path.GetFileName(postedFile.FileName);

            }

            else
            {
                model.IdProofName = model.FileName;
            }

            User obj = _employeeService.Add(model);

            int employeeId = obj.Id;
            if (employeeId > 0)
            {
                HostingEnvironment.QueueBackgroundWorkItem(cancellationToken => Save(model, employeeId));
                //Send mail function
                DoWork(model);



                return RedirectToAction("Index", "Employee");
            }
            return View();

        }

        public void DoWork(EmployeeViewModel model)
        {
            MailService mailService = new MailService();
            EmployeeNotificationEvent += mailService.EmailNotification_EmployeeAdded;


            MessageService messageService = new MessageService();
            EmployeeNotificationEvent += messageService.MessageNotification_EmployeeAdded;


            DatabaseService databaseService = new DatabaseService();
            EmployeeNotificationEvent += databaseService.DatabaseNotification_EmployeeAdded;

            OnEmployeeAdded(new Events.EmployeeNotificationEventArgs() { Employee = model });
        }

        private void Save(EmployeeViewModel model, int employeeId, CancellationToken cancellationToken = default(CancellationToken))
        {
            HttpPostedFileBase file = Request.Files[0];
            var folderName = "EMP-" + employeeId;
            string uploadRoot = Server.MapPath("~/Images/");
            string folder = string.Format(uploadRoot + "/{0}/", folderName);
            string filepath = string.Empty;
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            if (model != null && model.IdProofName != "")
            {
                filepath = folder + model.IdProofName;
                if (model.IdProofName != null)
                {
                    var path = Path.Combine(folder, model.IdProofName);
                    file.SaveAs(path);
                }
            }
        }


    }
}
