using Employee.DL.Implementation;
using Employees.DL.Implementation;
using Employees.DL.Interface;
using Unity;
using Unity.Extension;

namespace Employee.Utilities
{
    /// <summary>
    /// https://www.c-sharpcorner.com/article/dependency-injection-using-unity-resolve-dependency-of-dependencies/
    /// 
    /// https://www.codeproject.com/Tips/1157736/Unity-Dependency-Injection-with-N-Layer-Project
    /// </summary>
    public class DependencyInjectionExtension : UnityContainerExtension
    {
        protected override void Initialize()
        {
            Container.RegisterType<ICountryRepository, CountryRepository>();
            Container.RegisterType<IEmployeeRepository, EmployeeRepository>();
            Container.RegisterType<ITitleMasterRepository, TitleRepository>();
        }
    }
}
