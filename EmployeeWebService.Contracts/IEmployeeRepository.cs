using EmployeeWebService.Models;
using EmployeeWebService.Models.Entities;
using EmployeeWebService.Models.ViewModels;

namespace EmployeeWebService.Contracts;

public interface IEmployeeRepository
{
    public int AddEmployee(EmployeeRequestModel model);
    public void DeleteEmployee(int id);
    public bool IsExist(int id);
    public IEnumerable<EmployeeViewModel> GetEmployeesByCompanyId(int id);
    public IEnumerable<EmployeeViewModel> GetEmployeesByDepartmentId(int id);
}