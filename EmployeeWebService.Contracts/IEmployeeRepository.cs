using EmployeeWebService.Models.ResponseModels;
using EmployeeWebService.Models.ViewModels;

namespace EmployeeWebService.Contracts;

public interface IEmployeeRepository
{
    public Task<int> AddEmployeeAsync(EmployeeRequestModel model);
    public Task<int> DeleteEmployeeAsync(int id);
    public Task<bool>  IsExistAsync(int id);
    public Task<IEnumerable<EmployeeResponseModel>> GetEmployeesByCompanyIdAsync(int id);
    public Task<IEnumerable<EmployeeResponseModel>> GetEmployeesByDepartmentIdAsync(int id);
    public Task<int> UpdateEmployeeAsync(EmployeeUpdateModel model);
    public Task<bool> IsSamePassportExistAsync(int id);
}