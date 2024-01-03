using System.ComponentModel;
using EmployeeWebService.Contracts;
using EmployeeWebService.Models.ResponseModels;
using EmployeeWebService.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeWebService.API.Controllers
{
    [Route("/[controller]/")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentManager _departmentManager;
        public DepartmentController(IDepartmentManager departmentManager)
        {
            _departmentManager = departmentManager;
        }

        [HttpPost("")]

        [Description("Get department Id if it exist, if not, returns Id of created")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ExceptionResponseModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ExceptionResponseModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetOrAddDepartmentAsync(DepartmentRequestModel department)
        {
            int id = await _departmentManager.GetOrAddDepartmentAsync(department);
            return Ok(id);
        }
    }
}
