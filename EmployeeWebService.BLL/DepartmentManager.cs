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

    public async Task<int> GetOrAddDepartmentAsync(DepartmentRequestModel model)
    {
        return await _departmentRepository.GetDepartmentIdAsync(model) ?? await _departmentRepository.AddDepartmentAsync(model);
    }
}