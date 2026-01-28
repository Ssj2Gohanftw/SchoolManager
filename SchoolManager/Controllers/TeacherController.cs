using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using SchoolManager.Dtos.Student;
using SchoolManager.Dtos.Teacher;
using SchoolManager.Extensions;
using SchoolManager.Services.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace SchoolManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherServices _teacherServices;
        private readonly IValidator<AddTeacherDto> _validator;
        public TeacherController(ITeacherServices teacherServices, IValidator<AddTeacherDto> validator)
        {
            _teacherServices = teacherServices;
            _validator = validator;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllTeachers()
        {
            var allTeachers = await _teacherServices.GetAllAsync();
            return Ok(allTeachers);
        }

        [HttpPost]
        public async Task<IActionResult> AddTeacher(AddTeacherDto addTeacherDto)
        {
            var validationResult = await _validator.ValidateAsync(addTeacherDto);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState);
                return ValidationProblem(ModelState);
            }

            var teachers = await _teacherServices.AddTeacherAsync(addTeacherDto);
            return Ok(teachers);
        }
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateTeacher(UpdateTeacherDto updateTeacherDto, Guid id)
        {
            var success = await _teacherServices.UpdateTeacherAsync(id, updateTeacherDto);
            if (!success)
            {
                return NotFound();
            }
            return Ok();
        }

        [HttpDelete]
        [Route("{id:guid}")]

        public async Task<IActionResult> DeleteTeacher(Guid id)
        {

            var success = await _teacherServices.DeleteTeacherAsync(id);
            if (!success)
            {
                return NotFound();
            }
            return Ok();
        }
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetTeacherById(Guid id)
        {
            var teacher = await _teacherServices.GetTeacherByIdAsync(id);
            if (teacher == null)
            {
                return NotFound();
            }

            return Ok(teacher);
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetPagedResults([FromQuery] TeacherQueryDto teacherQueryDto)
        {
            var pagedResults = await _teacherServices.GetPagedTeachersAsync(teacherQueryDto);
            return Ok(pagedResults);
        }
    }
}
