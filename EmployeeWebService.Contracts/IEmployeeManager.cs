using EmployeeWebService.Models;
using EmployeeWebService.Models.ViewModels;

namespace EmployeeWebService.Contracts;

public interface IEmployeeManager
{
    public int AddEmployee(EmployeeRequestModel model);
    public int DeleteEmployee(int id);
    public IEnumerable<EmployeeResponseModel> GetEmployeesByCompanyId(int id);
    public IEnumerable<EmployeeResponseModel> GetEmployeesByDepartmentId(int id);

    public int UpdateEmployee(EmployeeUpdateModel model);
}