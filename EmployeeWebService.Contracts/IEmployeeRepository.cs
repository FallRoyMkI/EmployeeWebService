using EmployeeWebService.Models.Entities;

namespace EmployeeWebService.Contracts;

public interface IEmployeeRepository
{
    public int AddEmployee(Employee model);
    public void DeleteEmployee(int id);
    public bool IsExist(int id);
}