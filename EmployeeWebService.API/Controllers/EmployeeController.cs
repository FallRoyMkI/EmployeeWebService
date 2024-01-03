using System.ComponentModel;
using EmployeeWebService.Contracts;
using EmployeeWebService.Models;
using EmployeeWebService.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;


namespace EmployeeWebService.API.Controllers;
[Route("[controller]/")]
[ApiController]
public class EmployeeController : ControllerBase
{
    private readonly IEmployeeManager _employeeManager;

    public EmployeeController(IEmployeeManager employeeManager)
    {
        _employeeManager = employeeManager;
    }

    [HttpPost("")]
    [Description("Add new employee, returns Id")]
    [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Exception), StatusCodes.Status500InternalServerError)]
    public IActionResult AddEmployee(EmployeeRequestModel model)
    {
        return Ok(_employeeManager.AddEmployee(model));
    }

    [HttpDelete("")]
    [Description("Soft delete of employee")]
    [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ArgumentException), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Exception), StatusCodes.Status500InternalServerError)]
    public IActionResult DeleteEmployee(int id)
    {
        return Ok(_employeeManager.DeleteEmployee(id));
    }

    [HttpGet("company/{id}")]
    [Description("Get employees by company Id")]
    [ProducesResponseType(typeof(IEnumerable<EmployeeResponseModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Exception), StatusCodes.Status500InternalServerError)]
    public IActionResult GetEmployeesByCompanyId([FromRoute] int id)
    {
        return Ok(_employeeManager.GetEmployeesByCompanyId(id));
    }

    [HttpGet("department/{id}")]
    [Description("Get employees by department Id")]
    [ProducesResponseType(typeof(IEnumerable<EmployeeResponseModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Exception), StatusCodes.Status500InternalServerError)]
    public IActionResult GetEmployeesByDepartmentId([FromRoute] int id)
    {
        return Ok(_employeeManager.GetEmployeesByDepartmentId(id));
    }

    [HttpPatch("update")]
    [Description("Update employee by Id")]
    [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Exception), StatusCodes.Status500InternalServerError)]
    public IActionResult UpdateEmployee(EmployeeUpdateModel model)
    {
        return Ok(_employeeManager.UpdateEmployee(model));
    }
}