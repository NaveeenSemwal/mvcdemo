using Employee.BL.Interface;
using Employee.DL.Implementation;
using Employee.Model;
using Employees.DL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User = Employees.DL.Database.Employee;

namespace Employee.BL.Implementation
{
    public class EmployeeService : BaseService, IEmployeeService
    {

        public User Add(EmployeeViewModel model)
        {
            User obj = SaveEmployee(model);
            return obj;
        }

        private User SaveEmployee(EmployeeViewModel model)
        {
            User obj = model.AddUpdateEmployeeMapping(model);

            if (model.EmployeeId == 0)
            {
                _employeeRepository.Add(obj);
            }
            else if (model.EmployeeId > 0)
            {
                _employeeRepository.Update(obj);
            }

            return obj;
        }
        public EmployeeViewModel GetEmpById(int empId)
        {
            var employee = _employeeRepository.GetEmpById(empId);

            EmployeeViewModel viewModel = new EmployeeViewModel();


            viewModel.EmployeeId = employee.EmployeeId;
            viewModel.TitleId = employee.TitleId.Value;
            viewModel.FirstName = employee.FirstName;
            viewModel.LastName = employee.LastName;
            viewModel.Gender = employee.Gender;
            viewModel.Dob = employee.Dob.Value;
            viewModel.IdProofName = employee.IdProofName;
            viewModel.CountryId = employee.CountryId.Value;
            viewModel.Email = employee.Email;
            viewModel.Mobile = employee.Mobile;
            viewModel.IsActive = employee.IsActive.Value;
            viewModel.ImagePath = "E:/KIRAN/MVCDemo/MVCDemoEFr/Images/EMP-" + employee.EmployeeId + "/" + employee.IdProofName;
            viewModel.FileName = employee.IdProofName;

            return viewModel;

        }

        public List<EmployeeViewModel> GetList(string searchString, string sortOrders, int? page, int pageSize)
        {
            List<User> emplist = _employeeRepository.GetList(searchString, sortOrders, page, pageSize).ToList();


            List<EmployeeViewModel> employees = EmployeeViewModel.EmployeeListMapping(emplist);
            return employees;
        }

        public User Login(LoginViewModel model)
        {
            User obj = new User() { Email = model.Email, UserPassword = model.Password };
            User emp = _employeeRepository.Login(obj);
            return emp;
        }

        public ResetPasswordViewModel ResetPassword(int? id)
        {
            ResetPasswordViewModel model = null;
            var employee = _employeeRepository.ResetPassword(id);
            if (employee.EmployeeId > 0)
            {
                model = new ResetPasswordViewModel()
                {
                    EmployeeId = employee.EmployeeId,
                    Email = employee.Email,
                    IsActive = employee.IsActive.Value
                };

            }
            return model;
        }

        public EmployeeViewModel Update(EmployeeViewModel model)
        {
            throw new NotImplementedException();
        }

        public User UpdatePassword(ResetPasswordViewModel model)
        {
            User employee = new User()
            {
                EmployeeId = model.EmployeeId,
                UserPassword = model.ResetPassword,
                IsActive = model.IsActive
            };
            User emp = _employeeRepository.UpdatePassword(employee);
            return emp;
        }
    }
}
