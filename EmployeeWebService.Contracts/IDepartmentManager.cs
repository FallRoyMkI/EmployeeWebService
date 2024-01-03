using EmployeeWebService.Models.ViewModels;

namespace EmployeeWebService.Contracts;

public interface IDepartmentManager
{
    public Task<int> GetOrAddDepartmentAsync(DepartmentRequestModel model);
}