using Employees.DL.Implementation;
using Employees.DL.Interface;
using System;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using User = Employees.DL.Database.Employee;

namespace Employee.DL.Implementation
{
    public class EmployeeRepository : BaseRepository, IDisposable, IEmployeeRepository
    {
        public User Add(User model)
        {
            DbContext.Employees.Add(model);
            DbContext.SaveChanges();

            return new User();
        }


        public IQueryable<User> GetList(string searchString, string sortOrder, int? page, int pageSize)
        {
            // TODO : Move this logic to BL and use Predicate
            try
            {
                int skip = (((page.Value - 1) * pageSize) + 1) - 1;

                int take = page.Value * pageSize;

                var userlist = DbContext.Employees.AsQueryable();

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
            var employee = DbContext.Employees.Where(x => x.Id == empId).FirstOrDefault();

            return employee;

        }

        public User Update(User model)
        {
            User emp = new User();
            emp = DbContext.Employees.Where(x => x.Id == model.Id).FirstOrDefault();

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
                DbContext.Employees.Attach(emp);
                DbContext.Entry(emp).State = EntityState.Modified;
                DbContext.SaveChanges();
            }

            return new User();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public User ResetPassword(int? id)
        {
            User obj = new User();
            if (id != null)
            {
                obj = DbContext.Employees.Where(x => x.Id == id).SingleOrDefault();

            }
            return obj;
        }

        public User UpdatePassword(User model)
        {
            User emp = new User();
            emp = DbContext.Employees.Where(x => x.Id == model.Id).FirstOrDefault();

            if (emp != null)
            {
                emp.UserPassword = model.UserPassword;
                emp.IsActive = model.IsActive;
                emp.UpdatedOn = DateTime.Now;
                DbContext.Employees.Attach(emp);
                DbContext.Entry(emp).State = EntityState.Modified;
                DbContext.SaveChanges();
            }
            return emp;
        }

        public User Login(User model)
        {
            User emp = null;
            try
            {
                bool isExist = DbContext.Employees.Any(x => x.Email == model.Email && x.UserPassword == model.UserPassword);

                if (isExist)
                {
                    emp = new User();
                    emp = DbContext.Employees.Where(x => x.Email == model.Email && x.UserPassword == model.UserPassword).FirstOrDefault();
                }

            }
            catch(SqlException exc)
            {
                throw exc;
            }
            catch (Exception exc)
            {

                throw exc;
            }
            return emp;
        }
    }
}
