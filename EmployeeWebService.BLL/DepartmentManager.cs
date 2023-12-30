using EmployeeWebService.Contracts;
using EmployeeWebService.Models.ViewModels;

namespace EmployeeWebService.BLL;

public class DepartmentManager : IDepartmentManager
{
    private readonly IDepartmentRepository _departmentRepository;
    public DepartmentManager(IDepartmentRepository departmentRepository)
    {
        _departmentRepository = departmentRepository;
    }

    public int GetOrAddDepartment(DepartmentRequestModel model)
    {
        return _departmentRepository.GetDepartmentId(model) ?? _departmentRepository.AddDepartment(model);
    }
}