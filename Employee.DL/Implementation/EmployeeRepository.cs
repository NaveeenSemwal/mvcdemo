using Employees.DL.Database;
using Employees.DL.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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


        public IEnumerable<User> GetList(string searchString)
        {
            var userlist = dbContext.Employees.Take(20).ToList();
            if (!String.IsNullOrEmpty(searchString))
            {
                userlist = dbContext.Employees.Where(s => s.LastName.ToUpper().Contains(searchString.ToUpper())
                                       || s.FirstName.ToUpper().Contains(searchString.ToUpper())).Take(20).ToList();
            }

            return userlist;

        }


        public User GetEmpById(int empId)
        {
            var employee = dbContext.Employees.Where(x => x.EmployeeId == empId).FirstOrDefault();

            return employee;

        }
        public User Update(User model)
        {
            User emp = new User();
            emp = dbContext.Employees.Where(x => x.EmployeeId == model.EmployeeId).FirstOrDefault();

            if (emp != null)
            {
                emp.TitleId = model.TitleId;
                emp.FirstName = model.FirstName;
                emp.LastName = model.LastName;
                emp.Gender = model.Gender;
                emp.Dob = model.Dob;
                emp.IdProofName = model.IdProofName;
                emp.CountryId = model.CountryId;
                emp.IsActive = model.IsActive;
                emp.UpdatedOn = DateTime.Now;
                dbContext.Employees.Attach(emp);
                dbContext.Entry(emp).State = EntityState.Modified;
                dbContext.SaveChanges();
            }




            return new User();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }


    }
}
