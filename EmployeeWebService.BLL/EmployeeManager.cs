using EmployeeWebService.Contracts;
using EmployeeWebService.Models;
using EmployeeWebService.Models.ViewModels;

namespace EmployeeWebService.BLL
{
    public class EmployeeManager : IEmployeeManager
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IPassportRepository _passportRepository;
        public EmployeeManager(IEmployeeRepository employeeRepository,
            IDepartmentRepository departmentRepository, IPassportRepository passportRepository)
        {
            _employeeRepository = employeeRepository;
            _departmentRepository = departmentRepository;
            _passportRepository = passportRepository;
        }

        public int AddEmployee(EmployeeRequestModel model)
        {
            if (!_passportRepository.IsExist(model.DepartmentId))
            {
                throw new Exception("There is no passport with this id");
            }
            if (!_departmentRepository.IsExist(model.DepartmentId))
            {
                throw new Exception("There is no department with this id");
            }

            return _employeeRepository.AddEmployee(model);
        }

        public void DeleteEmployee(int id)
        {
            if (!_employeeRepository.IsExist(id))
            {
                throw new Exception("There is no employee with this id");
            }

            _employeeRepository.DeleteEmployee(id);
        }

        public IEnumerable<EmployeeViewModel> GetEmployeesByCompanyId(int id)
        {
            return _employeeRepository.GetEmployeesByCompanyId(id);
        }

        public IEnumerable<EmployeeViewModel> GetEmployeesByDepartmentId(int id)
        {
            return _employeeRepository.GetEmployeesByDepartmentId(id);
        }
    }
}
