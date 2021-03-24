using System.Web.Mvc;

namespace MVCDemoEFr.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// https://docs.microsoft.com/en-us/aspnet/mvc/overview/older-versions/getting-started-with-ef-5-using-mvc-4/sorting-filtering-and-paging-with-the-entity-framework-in-an-asp-net-mvc-application
        /// </summary>
        /// <returns></returns>



        // ems/home
        public ActionResult Index()
        {
            return View();
        }




    }

}