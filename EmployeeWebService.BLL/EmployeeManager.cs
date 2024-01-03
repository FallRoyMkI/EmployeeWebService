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

        public async Task<int> AddEmployeeAsync(EmployeeRequestModel model)
        {
            if (!await _passportRepository.IsExistAsync(model.DepartmentId))
            {
                throw new EntityNotFoundException("There is no passport with this id");
            }
            if (!await _departmentRepository.IsExistAsync(model.DepartmentId))
            {
                throw new EntityNotFoundException("There is no department with this id");
            }
            if (await _employeeRepository.IsSamePassportExistAsync(model.PassportId))
            {
                throw new DuplicateAddingAttemptedException("Employee with same passport already exist");
            }

            return await _employeeRepository.AddEmployeeAsync(model);
        }

        public async Task<bool> DeleteEmployeeAsync(int id)
        {
            if (!await _employeeRepository.IsExistAsync(id))
            {
                throw new EntityNotFoundException("There is no employee with this id");
            }

            var response = await _employeeRepository.DeleteEmployeeAsync(id);

            if (response == 1)
            {
                return true;
            }
            throw new MultipleUpdateException("Was updated more/less than 1 row");
        }

        public async Task<IEnumerable<EmployeeResponseModel>> GetEmployeesByCompanyIdAsync(int id)
        {
            return await _employeeRepository.GetEmployeesByCompanyIdAsync(id);
        }

        public async Task<IEnumerable<EmployeeResponseModel>> GetEmployeesByDepartmentIdAsync(int id)
        {
            if (!await _departmentRepository.IsExistAsync(id))
            {
                throw new EntityNotFoundException("There is no department with this id");
            }

            return await _employeeRepository.GetEmployeesByDepartmentIdAsync(id);
        }

        public async Task<bool> UpdateEmployeeAsync(EmployeeUpdateModel model)
        {
            if (!await _employeeRepository.IsExistAsync(model.Id))
            {
                throw new EntityNotFoundException("There is no employee with this id");
            }
            if (model.DepartmentId != null)
            {
                if (!await _departmentRepository.IsExistAsync((int)model.DepartmentId))
                {
                    throw new EntityNotFoundException("There is no department with this id");
                }
            }

            var response = await _employeeRepository.UpdateEmployeeAsync(model);

            if (response == 1)
            {
                return true;
            }
            throw new MultipleUpdateException("Was updated more/less than 1 row");
        }
    }
}
