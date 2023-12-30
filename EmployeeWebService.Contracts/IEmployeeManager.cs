using EmployeeWebService.Models;
using EmployeeWebService.Models.ViewModels;

namespace EmployeeWebService.Contracts;

public interface IEmployeeManager
{
    public int AddEmployee(EmployeeRequestModel model);
    public void DeleteEmployee(int id);
    public IEnumerable<EmployeeViewModel> GetEmployeesByCompanyId(int id);
}