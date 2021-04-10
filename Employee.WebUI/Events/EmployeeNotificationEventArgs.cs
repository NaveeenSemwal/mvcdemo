using Employee.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Employee.WebUI.Events
{
    public class EmployeeNotificationEventArgs : EventArgs
    {
        public EmployeeViewModel Employee { get; set; }
    }
}