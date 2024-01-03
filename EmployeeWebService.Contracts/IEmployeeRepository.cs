using EmployeeWebService.Models.ResponseModels;
using EmployeeWebService.Models.ViewModels;

namespace EmployeeWebService.Contracts;

public interface IEmployeeRepository
{
    public int AddEmployee(EmployeeRequestModel model);
    public int DeleteEmployee(int id);
    public bool IsExist(int id);
    public IEnumerable<EmployeeResponseModel> GetEmployeesByCompanyId(int id);
    public IEnumerable<EmployeeResponseModel> GetEmployeesByDepartmentId(int id);
    public int UpdateEmployee(EmployeeUpdateModel model);
    public bool IsSamePassportExist(int id);
}