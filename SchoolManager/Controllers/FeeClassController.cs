using Microsoft.AspNetCore.Mvc;
using SchoolManager.Dtos.Fee;
using SchoolManager.Services.Interfaces;

namespace SchoolManager.Controllers
{
    [ApiController]
    [Route("api/fee/class")]
    public class FeeClassController : ControllerBase
    {
        private readonly IFeeClassServices _feeClassServices;
        public FeeClassController(IFeeClassServices feeClassServices)
        {
            _feeClassServices = feeClassServices;
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetClassFeeDetailsByIdAsync(Guid id)
        {
            var feeDetails = await _feeClassServices.GetFeeDetailsByClassIdAsync(id);
            return Ok(feeDetails);
        }

        [HttpPost]
        [Route("assign")]
        public async Task<IActionResult> AssignFeesToClass([FromBody] AssignFeeToClassDto assignFeeToClassDto)
        {
            var result = await _feeClassServices.AssignFeeToClassAsync(assignFeeToClassDto);
            return Ok(result);
        }
    }
}
