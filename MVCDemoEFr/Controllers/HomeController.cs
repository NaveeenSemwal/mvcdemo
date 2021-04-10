using System.Web.Mvc;

namespace MVCDemoEFr.Controllers
{
    [RoutePrefix("ems")]
    public class HomeController : Controller
    {
        /// <summary>
        /// https://docs.microsoft.com/en-us/aspnet/mvc/overview/older-versions/getting-started-with-ef-5-using-mvc-4/sorting-filtering-and-paging-with-the-entity-framework-in-an-asp-net-mvc-application
        /// </summary>
        /// <returns></returns>

        // https://devblogs.microsoft.com/aspnet/attribute-routing-in-asp-net-mvc-5/

        // ems/home
        // Use a tilde (~) on the method attribute to override the route prefix if needed:
        [Route("~/")]
        [Route("home")]
        public ActionResult Index()
        {
            //MailService mailService = new MailService();
            //EmployeeNotification.EmployeeAdded += mailService.EmployeeNotification_EmployeeAdded;

            return View();
        }




    }

}