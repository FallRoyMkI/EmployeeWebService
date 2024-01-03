using System.Reflection;
using EmployeeWebService.Contracts;
using EmployeeWebService.Models.Exceptions;
using EmployeeWebService.Models.ResponseModels;
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
                throw new EntityNotFoundException("There is no passport with this id");
            }
            if (!_departmentRepository.IsExist(model.DepartmentId))
            {
                throw new EntityNotFoundException("There is no department with this id");
            }
            if (_employeeRepository.IsSamePassportExist(model.PassportId))
            {
                throw new DuplicateEmployeeAddingException("Employee with same passport already exist");
            }
            
            return _employeeRepository.AddEmployee(model);
        }

        public int DeleteEmployee(int id)
        {
            if (!_employeeRepository.IsExist(id))
            {
                throw new EntityNotFoundException("There is no employee with this id");
            }

            return _employeeRepository.DeleteEmployee(id);
        }

        public IEnumerable<EmployeeResponseModel> GetEmployeesByCompanyId(int id)
        {
            return _employeeRepository.GetEmployeesByCompanyId(id);
        }

        public IEnumerable<EmployeeResponseModel> GetEmployeesByDepartmentId(int id)
        {
            if (!_departmentRepository.IsExist(id))
            {
                throw new EntityNotFoundException("There is no department with this id");
            }

            return _employeeRepository.GetEmployeesByDepartmentId(id);
        }

        public int UpdateEmployee(EmployeeUpdateModel model)
        {
            if (!_employeeRepository.IsExist(model.Id))
            {
                throw new EntityNotFoundException("There is no employee with this id");
            }
            if (model.DepartmentId == null) return _employeeRepository.UpdateEmployee(model);
            if (!_departmentRepository.IsExist((int)model.DepartmentId))
            {
                throw new EntityNotFoundException("There is no department with this id");
            }
            return _employeeRepository.UpdateEmployee(model);
        }
    }
}
