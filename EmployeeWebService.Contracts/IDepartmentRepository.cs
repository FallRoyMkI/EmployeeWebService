using EmployeeWebService.Models.ViewModels;

namespace EmployeeWebService.Contracts;

public interface IDepartmentRepository
{
    public int? GetDepartmentId(DepartmentRequestModel model);
    public int AddDepartment(DepartmentRequestModel model);
    public bool IsExist(int id);
}