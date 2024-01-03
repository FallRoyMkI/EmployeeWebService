using EmployeeWebService.Models.ResponseModels;
using EmployeeWebService.Models.ViewModels;

namespace EmployeeWebService.Contracts;

public interface IEmployeeManager
{
    public Task<int> AddEmployeeAsync(EmployeeRequestModel model);
    public Task<bool> DeleteEmployeeAsync(int id);
    public Task<IEnumerable<EmployeeResponseModel>> GetEmployeesByCompanyIdAsync(int id);
    public Task<IEnumerable<EmployeeResponseModel>> GetEmployeesByDepartmentIdAsync(int id);
    public Task<bool> UpdateEmployeeAsync(EmployeeUpdateModel model);
}