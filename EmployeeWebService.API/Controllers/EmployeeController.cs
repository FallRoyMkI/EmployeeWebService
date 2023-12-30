using EmployeeWebService.Contracts;
using EmployeeWebService.Models;
using EmployeeWebService.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeWebService.API.Controllers;
[Route("[controller]")]
[ApiController]
public class EmployeeController : ControllerBase
{
    private readonly IEmployeeManager _employeeManager;

    public EmployeeController(IEmployeeManager employeeManager)
    {
        _employeeManager = employeeManager;
    }

    [HttpPost("/add")]
    public IActionResult AddEmployee(EmployeeRequestModel model)
    {
        return Ok(_employeeManager.AddEmployee(model));
    }

    [HttpDelete("/delete")]
    public IActionResult DeleteEmployee(int id)
    {
        _employeeManager.DeleteEmployee(id);
        return Ok();
    }

    [HttpGet("/company/{id}")]
    public IActionResult GetEmployeesByCompanyId([FromRoute] int id)
    {
        return Ok(_employeeManager.GetEmployeesByCompanyId(id));
    }

    [HttpGet("/department/")]
    public IActionResult GetEmployeesByDepartment([FromQuery] DepartmentViewModel model)
    {
        return Ok(_employeeManager.GetEmployeesByDepartment(model));
    }

    [HttpPatch("/update")]
    public IActionResult UpdateEmployee()
    {
        return Ok();
    }
}