using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Employee.Utilities
{
    public class MailService
    {
        public void EmailNotification_EmployeeAdded(object source, EventArgs eventArgs)
        {
            Thread.Sleep(35000);

            //Task.Run(async () => {
            //    using (var smtp = new SmtpClient())
            //    {
            //        var credential = new NetworkCredential
            //        {
            //            UserName = "email@gmail.com",
            //            Password = "paswordEmail"
            //        };
            //        smtp.Credentials = credential;
            //        smtp.Host = "smtp-mail.outlook.com";
            //        smtp.Port = 587;
            //        smtp.EnableSsl = true;
            //        await smtp.SendMailAsync(message); //Here is my await function for sending an email
            //    }
            //});
        }
    }
}
