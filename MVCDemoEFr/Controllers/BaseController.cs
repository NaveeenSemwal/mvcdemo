using Employee.BL.Implementation;
using Employee.BL.Interface;
using Employee.DL.Implementation;
using Employees.DL.Implementation;
using Employees.DL.Interface;
using MVCDemoEFr.Events;
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

       
       

        public delegate void AddEmployeeNotificationHandler(object sender, EmployeeNotificationEventArgs eventArgs);


        public event AddEmployeeNotificationHandler EmployeeNotificationEvent;




        protected virtual void OnEmployeeAdded(EmployeeNotificationEventArgs eventArgs)
        {
            EmployeeNotificationEvent?.Invoke(this, eventArgs);
               
           
        }

        public BaseController()
        {
            _employeeservice = new EmployeeService();
            _countryservice = new CountryService();
            _titleservice = new TitleService();

           
            

        }
    }
}