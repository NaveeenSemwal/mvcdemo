using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using User = Employees.DL.Database.Employee;

namespace Employees.DL.Interface
{
    public interface IEmployeeRepository
    {
        User Add(User obj);

        IQueryable<User> GetList(string searchString, string sortOrders, int? page, int pageSize);

        User GetEmpById(int empId);

        User Update(User model);

        User ResetPassword(int? id);

        User UpdatePassword(User model);

        User Login(User model);
    }
}
