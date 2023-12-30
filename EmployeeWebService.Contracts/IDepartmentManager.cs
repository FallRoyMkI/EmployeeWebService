using EmployeeWebService.Models.ViewModels;

namespace EmployeeWebService.Contracts;

public interface IDepartmentManager
{
    public int GetOrAddDepartment(DepartmentRequestModel model);
}