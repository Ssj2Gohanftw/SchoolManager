using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using SchoolManager.Dtos.Class;
using SchoolManager.Extensions;
using SchoolManager.Services.Interfaces;

namespace SchoolManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClassController : ControllerBase
    {
        private readonly IClassServices _classServices;
        private readonly IValidator<AddClassDto> _validator;
        public ClassController(IClassServices classServices, IValidator<AddClassDto> validator)
        {
            _classServices = classServices;
            _validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllClasses()
        {
            var classes = await _classServices.GetAllAsync();
            return Ok(classes);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetClassById(Guid id)
        {
            var _class = await _classServices.GetClassByIdAsync(id);
            if (_class == null)
            {
                return NotFound();
            }
            return Ok(_class);
        }

        [HttpPost]
        public async Task<IActionResult> AddClass(AddClassDto addClassDto)
        {
            var validationResult = await _validator.ValidateAsync(addClassDto);
            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState);
                return ValidationProblem(ModelState);
            }
            var _class = await _classServices.AddClassAsync(addClassDto);

            return Ok(_class);
        }

        [HttpPut]
        [Route("{id:guid}")]

        public async Task<IActionResult> UpdateClass(Guid id, UpdateClassDto updateClassDto)
        {
            var success = await _classServices.UpdateClassAsync(id, updateClassDto);
            if (!success)
            {
                return NotFound();
            }
            return Ok();
        }

        [HttpDelete]
        [Route("{id:guid}")]

        public async Task<IActionResult> DeleteClass(Guid id)
        {
            var success = await _classServices.DeleteClassAsync(id);
            if (!success)
            {
                return NotFound();
            }
            return Ok();

        }
        [HttpGet("list")]
        public async Task<IActionResult> GetClassesPaged([FromQuery] ClassQueryDto classQueryDto)
        {

            var result = await _classServices.GetPagedClassesAsync(classQueryDto);
            return Ok(result);
        }
    }
}
