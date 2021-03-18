using Employees.DL.Database;
using Employees.DL.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
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


        public IQueryable<User> GetList(string searchString, string sortOrder, int? page, int pageSize)
        {
            try
            {
                int skip = (((page.Value - 1) * pageSize) + 1) - 1;

                int take = page.Value * pageSize;

                var userlist = dbContext.Employees.AsQueryable();

                var criteria = GetFilterCriteria(sortOrder);

                DateTime maxcreatedOn = userlist.Max(x => x.CreatedOn).Value;
                DateTime maxUpdatedOn = userlist.Max(x => x.UpdatedOn).Value;


                // Whicherver is the created/updated date maximum, select reccord by them DESC order.

                if (maxcreatedOn > maxUpdatedOn)
                {
                    criteria.sortWithDate = x => x.CreatedOn;
                }
                else
                {
                    criteria.sortWithDate = x => x.UpdatedOn;
                }

                userlist = SortEmployees(sortOrder, criteria.sortWithDate, criteria.sortWithOutDate, userlist);

                userlist = userlist.Skip(skip).Take(take);

                userlist = SearchEmployees(searchString, userlist);

                return userlist;
            }
            catch (Exception ex)
            {

                throw ex;

            }

        }


        private static (Expression<Func<User, DateTime?>> sortWithDate, Expression<Func<User, string>> sortWithOutDate) GetFilterCriteria(string sortOrder)
        {
            Expression<Func<User, DateTime?>> dateExpression = null;

            Expression<Func<User, string>> stringExpression = null;


            switch (sortOrder)
            {
                case "FirstName_desc":
                    stringExpression = x => x.FirstName;
                    break;
                case "UpdatedOn":
                    dateExpression = x => x.UpdatedOn;
                    break;
                case "CreatedDate_desc":
                    dateExpression = x => x.CreatedOn;
                    break;
                default:
                    dateExpression = x => x.CreatedOn;
                    break;
            }

            return (dateExpression, stringExpression);
        }


        private static IQueryable<User> SearchEmployees(string searchString, IQueryable<User> userlist)
        {
            if (!String.IsNullOrEmpty(searchString))
            {
                userlist = userlist.Where(s => s.LastName.ToUpper().Contains(searchString.ToUpper())
                                         || s.FirstName.ToUpper().Contains(searchString.ToUpper()));
            }

            return userlist;
        }

        private static IQueryable<User> SortEmployees(string sortOrder, Expression<Func<User, DateTime?>> sortWithDate,
            Expression<Func<User, string>> sortWithOutDate, IQueryable<User> userlist)
        {
            switch (sortOrder)
            {
                case "FirstName_desc":
                    userlist = userlist.OrderByDescending(sortWithOutDate);
                    break;

                case "UpdatedOn":
                case "CreatedDate_desc":
                    userlist = userlist.OrderByDescending(sortWithDate);
                    break;

                default:
                    userlist = userlist.OrderByDescending(sortWithDate);
                    break;
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
                if (model.Mobile != null)
                {
                    emp.Mobile = model.Mobile;
                }
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
