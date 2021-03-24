using Employee.BL.Implementation;
using Employee.BL.Interface;
using Employee.DL.Implementation;
using Employees.DL.Implementation;
using Employees.DL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCDemoEFr.Controllers
{
    [RoutePrefix("ems")]
    public class BaseController : Controller
    {

        protected readonly IEmployeeService _employeeservice;
        protected readonly ICountryService _countryservice;
        protected readonly ITitleService _titleservice;

        public BaseController()
        {
            _employeeservice = new EmployeeService();
            _countryservice = new CountryService();
            _titleservice = new TitleService();
        }
    }
}