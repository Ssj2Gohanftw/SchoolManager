using Microsoft.AspNetCore.Mvc;
using SchoolManager.Models.Dtos.Teacher;
using SchoolManager.Services;
using SchoolManager.Services.Interfaces;

namespace SchoolManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherService _teacherServices;
        public TeacherController(ITeacherService teacherServices)
        {
            _teacherServices = teacherServices;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllTeachers(CancellationToken cancellationToken)
        {
            var allTeachers = await _teacherServices.GetAllAsync(cancellationToken);
            return Ok(allTeachers);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetTeacherById(Guid id,CancellationToken cancellationToken)
        {
            var teachers = await _teacherServices.GetStudentByIdAsync(id,cancellationToken);
            if (teachers is null)
            {
                return NotFound();
            }
            return Ok(teachers);
        }
        [HttpPost]
        public async Task<IActionResult> AddTeacher(AddTeacherDto addTeacherDto,CancellationToken cancellationToken)
        {
            var teachers= await _teacherServices.AddTeacherAsync(addTeacherDto,cancellationToken);
            return Ok(teachers);
        }
        [HttpPut]
        [Route("{id:guid}")]

        public async Task<IActionResult> UpdateTeacher(UpdateTeacherDto updateTeacherDto, Guid id,CancellationToken cancellationToken)
        {
            var success = await _teacherServices.UpdateTeacherAsync(id,updateTeacherDto,cancellationToken);
            if (!success)
            {
                return NotFound();
            }
            return Ok();
        }

        [HttpDelete]
        [Route("{id:guid}")]

        public async Task<IActionResult> DeleteTeacher(Guid id,CancellationToken cancellationToken)
        {
            var success = await _teacherServices.DeleteTeacherAsync(id, cancellationToken);
            if (!success)
            {
                return NotFound();
            }
            await _teacherServices.DeleteTeacherAsync(id, cancellationToken);
            return Ok();

        }
    }
}
