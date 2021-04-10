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
        }
    }
}
