using EmployeeWebService.Contracts;
using EmployeeWebService.Models.RequestModels;
using EmployeeWebService.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeWebService.API.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class PassportController : ControllerBase
    {
        private readonly IPassportManager _passportManager;
        public PassportController(IPassportManager passportManager)
        {
            _passportManager = passportManager;
        }

        [HttpPost("")]
        public IActionResult GetOrAddPassport(PassportRequestModel passport)
        {
            return Ok(_passportManager.GetOrAddPassport(passport));
        }

        [HttpPost("/update")]
        public IActionResult UpdatePassportById(PassportUpdateModel passport)
        {
            _passportManager.UpdatePassport(passport);
            return Ok();
        }
    }
}
