using Employees.DL.Interface;

namespace Employee.BL.Implementation
{
    public abstract class BaseService
    {
        protected readonly IEmployeeRepository _employeeRepository;
        protected readonly ICountryRepository _countryRepository;
        protected readonly ITitleMasterRepository _titleRepository;

        public BaseService(IEmployeeRepository employeeRepository, ICountryRepository countryRepository, ITitleMasterRepository titleRepository)
        {
            _employeeRepository = employeeRepository;
            _countryRepository = countryRepository;
            _titleRepository = titleRepository;
        }
    }
}
