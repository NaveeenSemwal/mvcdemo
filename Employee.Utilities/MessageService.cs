using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Employee.Utilities
{
    public class MessageService
    {
        public void MessageNotification_EmployeeAdded(object source, EventArgs eventArgs)
        {
            Thread.Sleep(15000);
        }
    }
}
