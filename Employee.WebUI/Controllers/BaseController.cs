using Employee.BL.Implementation;
using Employee.BL.Interface;
using Employee.WebUI.Events;
using System.Web.Mvc;

namespace Employee.WebUI.Controllers
{
 
    public abstract class BaseController : Controller
    {
        protected readonly IEmployeeService _employeeService;
        protected readonly ICountryService _countryService;
        protected readonly ITitleService _titleService;

        public BaseController(IEmployeeService employeeService, ICountryService countryService, ITitleService titleService)
        {
            _employeeService = employeeService;
            _countryService = countryService;
            _titleService = titleService;
        }


        public delegate void AddEmployeeNotificationHandler(object sender, EmployeeNotificationEventArgs eventArgs);

        public event AddEmployeeNotificationHandler EmployeeNotificationEvent;

        protected virtual void OnEmployeeAdded(EmployeeNotificationEventArgs eventArgs)
        {
            EmployeeNotificationEvent?.Invoke(this, eventArgs);
        }
    }
}