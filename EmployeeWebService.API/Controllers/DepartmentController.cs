using System.ComponentModel;
using EmployeeWebService.Contracts;
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
        [ProducesResponseType(typeof(Exception), StatusCodes.Status500InternalServerError)]
        public IActionResult GetOrAddDepartment(DepartmentRequestModel department)
        {
            return Ok(_departmentManager.GetOrAddDepartment(department));
        }
    }
}
