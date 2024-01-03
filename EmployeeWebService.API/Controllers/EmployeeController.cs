using System.ComponentModel;
using EmployeeWebService.Contracts;
using EmployeeWebService.Models.ResponseModels;
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
    [ProducesResponseType(typeof(ExceptionResponseModel), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ExceptionResponseModel), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ExceptionResponseModel), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> AddEmployeeAsync(EmployeeRequestModel model)
    {
        int id = await _employeeManager.AddEmployeeAsync(model);
        return Ok(id);
    }

    [HttpDelete("")]
    [Description("Soft delete of employee")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ExceptionResponseModel), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ExceptionResponseModel), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ExceptionResponseModel), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteEmployeeAsync(int id)
    {
        bool result = await _employeeManager.DeleteEmployeeAsync(id);
        return Ok(result);
    }

    [HttpGet("company/{id}")]
    [Description("Get employees by company Id")]
    [ProducesResponseType(typeof(IEnumerable<EmployeeResponseModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ExceptionResponseModel), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ExceptionResponseModel), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ExceptionResponseModel), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetEmployeesByCompanyIdAsync([FromRoute] int id)
    {
        IEnumerable<EmployeeResponseModel> result = await _employeeManager.GetEmployeesByCompanyIdAsync(id);
        return Ok(result);
    }

    [HttpGet("department/{id}")]
    [Description("Get employees by department Id")]
    [ProducesResponseType(typeof(IEnumerable<EmployeeResponseModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ExceptionResponseModel), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ExceptionResponseModel), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ExceptionResponseModel), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetEmployeesByDepartmentIdAsync([FromRoute] int id)
    {
        IEnumerable<EmployeeResponseModel> result = await _employeeManager.GetEmployeesByDepartmentIdAsync(id);
        return Ok(result);
    }

    [HttpPatch("update")]
    [Description("Update employee by Id")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ExceptionResponseModel), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ExceptionResponseModel), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ExceptionResponseModel), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateEmployeeAsync(EmployeeUpdateModel model)
    {
        bool result = await _employeeManager.UpdateEmployeeAsync(model);
        return Ok(result);
    }
}