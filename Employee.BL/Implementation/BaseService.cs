using Employee.DL.Implementation;
using Employees.DL.Implementation;
using Employees.DL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.BL.Implementation
{
    public abstract class BaseService
    {
        protected readonly IEmployeeRepository _employeeRepository;

        protected readonly ICountryRepository _countryRepository;

        protected readonly ITitleMasterRepository _titleRepository;

        public BaseService()
        {
            _employeeRepository = new EmployeeRepository();
            _countryRepository = new CountryRepository();
            _titleRepository = new TitleRepository();
        }
    }
}
