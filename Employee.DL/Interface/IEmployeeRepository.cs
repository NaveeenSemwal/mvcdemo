using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User = Employees.DL.Database.Employee;

namespace Employees.DL.Interface
{
    public interface IEmployeeRepository
    {
        User Add(User obj);
        IEnumerable<User> GetList();
    }
}
