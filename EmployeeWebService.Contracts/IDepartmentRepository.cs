using EmployeeWebService.Models.Entities;

namespace EmployeeWebService.Contracts;

public interface IDepartmentRepository
{
    public bool IsExist(Department model);
    public void AddDepartment(Department model);
    public int GetDepartmentId(Department model);
}