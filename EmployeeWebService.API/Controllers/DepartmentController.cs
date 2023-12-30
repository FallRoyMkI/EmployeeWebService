using EmployeeWebService.Contracts;
using EmployeeWebService.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeWebService.API.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentManager _departmentManager;
        public DepartmentController(IDepartmentManager departmentManager)
        {
            _departmentManager = departmentManager;
        }

        [HttpPost("")]
        public IActionResult GetOrAddDepartment(DepartmentRequestModel department)
        {
            return Ok(_departmentManager.GetOrAddDepartment(department));
        }
    }
}
