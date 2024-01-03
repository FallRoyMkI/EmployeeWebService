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
        public async Task<IActionResult> GetOrAddPassportAsync(PassportRequestModel passport)
        {
            int id = await _passportManager.GetOrAddPassportAsync(passport);
            return Ok(id);
        }

        [HttpPost("update")]
        [Description("Update passport by id")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ExceptionResponseModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ExceptionResponseModel), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ExceptionResponseModel), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdatePassportByIdAsync(PassportUpdateModel passport)
        {
            bool result = await _passportManager.UpdatePassportAsync(passport);
            return Ok(result);
        }
    }
}
