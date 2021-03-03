using Employees.DL.Database;
using Employees.DL.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User = Employees.DL.Database.Employee;

namespace Employee.DL.Implementation
{
    public class EmployeeRepository : IDisposable, IEmployeeRepository
    {
        EMSDemoEntities dbContext = new EMSDemoEntities();
        public User Add(User model)
        {

            dbContext.Employees.Add(model);
            dbContext.SaveChanges();

            return new User();
        }


        public IEnumerable<User> GetList()
        {
            var userlist = dbContext.Employees.ToList();
            return userlist;

        }


        public void Dispose()
        {
            throw new NotImplementedException();
        }


    }
}
