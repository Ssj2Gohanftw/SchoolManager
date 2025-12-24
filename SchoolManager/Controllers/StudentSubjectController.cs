using Microsoft.AspNetCore.Mvc;
using SchoolManager.Models.Dtos.StudentSubject;
using SchoolManager.Services.Interfaces;

namespace SchoolManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentSubjectController : ControllerBase
    {
        private readonly IStudentSubjectServices _studentSubjectServices;

        public StudentSubjectController(IStudentSubjectServices studentSubjectServices)
        {
            _studentSubjectServices = studentSubjectServices;
        }

        [HttpGet("{studentId:guid}")]
        public async Task<IActionResult> GetSubjectsForStudent(Guid studentId)
        {
            var subjects = await _studentSubjectServices.GetSubjectsForStudentAsync(studentId);
            return Ok(subjects);
        }

        [HttpPost]
        public async Task<IActionResult> AssignSubject([FromBody] AddStudentSubjectDto dto)
        {
            await _studentSubjectServices.AssignSubjectToStudentAsync(dto);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> UnassignSubject([FromBody] DeleteStudentSubjectDto dto)
        {
            if (dto is null) return BadRequest();

            var success = await _studentSubjectServices.RemoveSubjectFromStudentAsync(dto);
            if (!success) return NotFound();

            return Ok();
        }
    }
}
