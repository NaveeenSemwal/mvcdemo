using Employee.Model;
using Employee.WebUI.Properties;
using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using User = Employees.DL.Database.Employee;

namespace Employee.WebUI.Controllers
{
    [RoutePrefix("ems")]
    public class AccountController : BaseController
    {
        // TODO : Implement forget password 


        [Route("register/password/{id:int}")]
        [HttpGet]
        public ActionResult ResetPassword(int? id)
        {
            if (!id.HasValue)
                throw new Exception(Resources.IdNullInRegisterPassword);

            var model = _employeeservice.ResetPassword(id);
            if (model != null)
            {
                return View(model);
            }
            else
            {
                return View();
            }
        }

        [Route("register/password")]
        [HttpPost]
        public ActionResult ResetPassword(ResetPasswordViewModel obj)
        {
            try
            {

                User emp = _employeeservice.UpdatePassword(obj);
                if (emp.Id > 0)
                {
                    return RedirectToAction("Login");
                }
                else
                {
                    return View();
                }
            }
            catch (Exception ex)
            {

                throw ex;

            }
        }


        [Route("employee/login")]
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

       

        [Route("employee/login")]
        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }


            User emp = _employeeservice.Login(model);

            if (emp != null)
            {
                string role = emp.IsAdmin != null ? "Admin" : "Employee";

                FormsAuthentication.SetAuthCookie(emp.FirstName, false);

                var authTicket = new FormsAuthenticationTicket(1, emp.Email, DateTime.Now, DateTime.Now.AddMinutes(20), false, role);
                string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                HttpContext.Response.Cookies.Add(authCookie);

                return RedirectToAction("Index", "Employee");
            }
            else
            {
                ModelState.AddModelError("", "Invalid login attempt.");
                return View(model);
            }

        }

        [Route("employee/logout")]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }
    }
}