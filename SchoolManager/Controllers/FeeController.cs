using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using SchoolManager.Dtos.Fee;
using SchoolManager.Extensions;
using SchoolManager.Services.Interfaces;

namespace SchoolManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FeeController : ControllerBase
    {
        private readonly IFeeServices _feeServices;
        private readonly IValidator<AddFeeDto> _addFeeValidator;
        private readonly IValidator<UpdateFeeDto> _updatefeeValidator;

        public FeeController(IFeeServices feeServices,
                             IValidator<AddFeeDto> addFeeValidator,
                             IValidator<UpdateFeeDto> updatefeeValidator)
        {
            _feeServices = feeServices;
            _addFeeValidator = addFeeValidator;
            _updatefeeValidator = updatefeeValidator;
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
            var validationResult = await _addFeeValidator.ValidateAsync(addFeeDto);
            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState);
                return ValidationProblem(ModelState);
            }
            var fee = await _feeServices.AddFeeAsync(addFeeDto);
            if (fee == null) return NotFound();
            return Ok(fee);
        }
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateFee(Guid id,UpdateFeeDto updateFeeDto)
        {
            var validationResult = await _updatefeeValidator.ValidateAsync(updateFeeDto);
            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState);
                return ValidationProblem(ModelState);
            }
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
