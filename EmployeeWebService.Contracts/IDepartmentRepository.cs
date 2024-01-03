using EmployeeWebService.Models.ViewModels;

namespace EmployeeWebService.Contracts;

public interface IDepartmentRepository
{
    public Task<int?> GetDepartmentIdAsync(DepartmentRequestModel model);
    public Task<int> AddDepartmentAsync(DepartmentRequestModel model);
    public Task<bool> IsExistAsync(int id);
}