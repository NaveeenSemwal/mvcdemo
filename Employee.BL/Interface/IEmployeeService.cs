using Employee.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User = Employees.DL.Database.Employee;

namespace Employee.BL.Interface
{
    public interface IEmployeeService
    {
        User Add(EmployeeViewModel obj);
        List<EmployeeViewModel> GetList(string searchString, string sortOrders, int? page, int pageSize);

        EmployeeViewModel GetEmpById(int empId);

        EmployeeViewModel Update(EmployeeViewModel model);

        ResetPasswordViewModel ResetPassword(int? id);

        User UpdatePassword(ResetPasswordViewModel model);

        User Login(LoginViewModel model);
    }
}
