using EmployeeWebService.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeWebService.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class EmployeeController : ControllerBase
{
    public EmployeeController()
    {

    }

    [HttpPost(Name = "AddEmployee")]
    public int AddEmployee()
    {
        return 0;
    }

    [HttpDelete(Name = "DeleteEmployee")]
    public void DeleteEmployee()
    {

    }

    [HttpGet(Name = "GetEmployeesByCompanyId")]
    public IEnumerable<Employee> GetEmployeesByCompanyId()
    {
        return new List<Employee> ();
    }

    [HttpGet(Name = "GetEmployeesByDepartmentId")]
    public IEnumerable<Employee> GetEmployeesByDepartmentId()
    {
        return new List<Employee>();
    }

    [HttpPatch(Name = "UpdateEmployee")]
    public void UpdateEmployee()
    {

    }
}