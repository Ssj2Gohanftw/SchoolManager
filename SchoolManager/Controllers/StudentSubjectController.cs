using Microsoft.AspNetCore.Mvc;
using SchoolManager.Dtos.StudentSubject;
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
            try
            {
                var subjects = await _studentSubjectServices.GetSubjectsForStudentAsync(studentId);
                return Ok(subjects);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AssignSubject([FromBody] AddStudentSubjectDto dto)
        {
            try
            {
                await _studentSubjectServices.AssignSubjectToStudentAsync(dto);
                return Ok();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost("class/{classId:guid}/assign")]
        public async Task<IActionResult> AssignSubjectsToClass(Guid classId, [FromBody] AssignSubjectsToClassDto dto)
        {
            try
            {
                await _studentSubjectServices.AssignSubjectsToClassAsync(classId, dto);
                return Ok();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> UnassignSubject([FromBody] DeleteStudentSubjectDto dto)
        {
            try
            {
                if (dto is null) return BadRequest();

                var success = await _studentSubjectServices.RemoveSubjectFromStudentAsync(dto);
                if (!success) return NotFound();

                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
