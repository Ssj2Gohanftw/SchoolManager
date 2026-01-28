using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using SchoolManager.Dtos.Class;
using SchoolManager.Dtos.Subject;
using SchoolManager.Extensions;
using SchoolManager.Services.Interfaces;

namespace SchoolManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubjectController : ControllerBase
    {
        private readonly ISubjectServices _subjectServices;
        private readonly IValidator<AddSubjectDto> _validator;
        public SubjectController(ISubjectServices subjectServices, IValidator<AddSubjectDto> validator)
        {
            _subjectServices = subjectServices;
            _validator = validator;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllSubjects()
        {

            var allSubjects = await _subjectServices.GetAllAsync();
            return Ok(allSubjects);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetSubjectById(Guid id)
        {
            var subject = await _subjectServices.GetSubjectByIdAsync(id);
            if (subject == null)
            {
                return NotFound();
            }
            return Ok(subject);
        }
        [HttpPost]
        public async Task<IActionResult> AddSubject(AddSubjectDto addSubjectDto)
        {
            var validationResult = await _validator.ValidateAsync(addSubjectDto);
            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState);
                return ValidationProblem(ModelState);
            }
            var subject = await _subjectServices.AddSubjectAsync(addSubjectDto);
            return Ok(subject);
        }
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateSubject(Guid id, UpdateSubjectDto updateSubjectDto)
        {
            var success = await _subjectServices.UpdateSubjectAsync(id, updateSubjectDto);
            if (!success)
            {
                return NotFound();
            }
            return Ok();
        }

        [HttpDelete]
        [Route("{id:guid}")]

        public async Task<IActionResult> DeleteSubject(Guid id)
        {
            var success = await _subjectServices.DeleteSubjectAsync(id);
            if (!success)
            {
                return NotFound();
            }
            return Ok();
        }
        [HttpGet("list")]
        public async Task<IActionResult> GetPagedSubjectsAsync([FromQuery] SubjectQueryDto subjectQueryDto)
        {
            var result = await _subjectServices.GetPagedSubjectsAsync(subjectQueryDto);
            return Ok(result);
        }
    }
}
