using EmployeeWebService.Models.ViewModels;

namespace EmployeeWebService.Contracts;

public interface IEmployeeManager
{
    public int AddEmployee(EmployeeViewModel model);
}