using System.ComponentModel;
using EmployeeWebService.Contracts;
using EmployeeWebService.Models.RequestModels;
using EmployeeWebService.Models.ResponseModels;
using EmployeeWebService.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeWebService.API.Controllers
{
    [Route("/[controller]/")]
    [ApiController]
    public class PassportController : ControllerBase
    {
        private readonly IPassportManager _passportManager;
        public PassportController(IPassportManager passportManager)
        {
            _passportManager = passportManager;
        }

        [HttpPost("")]
        [Description("Get passport Id if it exist, if not, returns Id of created")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ExceptionResponseModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ExceptionResponseModel), StatusCodes.Status500InternalServerError)]
        public IActionResult GetOrAddPassport(PassportRequestModel passport)
        {
            return Ok(_passportManager.GetOrAddPassport(passport));
        }

        [HttpPost("update")]
        [Description("Update passport by id")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ExceptionResponseModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ExceptionResponseModel), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ExceptionResponseModel), StatusCodes.Status404NotFound)]
        public IActionResult UpdatePassportById(PassportUpdateModel passport)
        {
            return Ok(_passportManager.UpdatePassport(passport));
        }
    }
}
