using Employee.BL.Implementation;
using Employee.BL.Interface;
using Employee.WebUI.Events;
using System.Web.Mvc;

namespace Employee.WebUI.Controllers
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