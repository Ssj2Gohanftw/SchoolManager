using Microsoft.AspNetCore.Mvc;
using SchoolManager.Dtos.Fee;
using SchoolManager.Services.Interfaces;

namespace SchoolManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FeeController : ControllerBase
    {
        private readonly IFeeServices _feeServices;

        public FeeController(IFeeServices feeServices)
        {
            _feeServices = feeServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllFees()
        {
            var fees = await _feeServices.GetAllFeesAsync();
            if (fees == null || !fees.Any()) return NoContent();
            return Ok(fees);
        }
        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetByFeeId(Guid id)
        {
            var fee = await _feeServices.GetByFeeId(id);
            if (fee == null) return NotFound();
            return Ok(fee);
        }
        [HttpPost]
        public async Task<IActionResult> AddFee(AddFeeDto addFeeDto)
        {
            var fee = await _feeServices.AddFeeAsync(addFeeDto);
            if (fee == null) return NotFound();
            return Ok(fee);
        }
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateFee(Guid id,UpdateFeeDto updateFeeDto)
        {
            var result = await _feeServices.UpdateFeeAsync(id,updateFeeDto);
            if (result == false) return NotFound();
            return Ok();
        }


        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> RemoveFee(Guid id)
        {
            var result = await _feeServices.RemoveFeeAsync(id);
            if (result == false) return NotFound();
            return Ok();
        }

    }
}
