using Microsoft.AspNetCore.Mvc;
using SchoolManager.Dtos.SubjectTeacher;
using SchoolManager.Services.Interfaces;

namespace SchoolManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubjectTeacherController : ControllerBase
    {
        private readonly ISubjectTeacherServices _subjectTeacherServices;

        public SubjectTeacherController(ISubjectTeacherServices subjectTeacherServices)
        {
            _subjectTeacherServices = subjectTeacherServices;
        }
        [HttpGet]
        [Route("teacher/{teacherId:guid}")]
        public async Task<IActionResult> GetAssignmentForTeachers(Guid teacherId)
        {
            try
            {
                var teacherAssignment = await _subjectTeacherServices.GetAssignmentsForTeacherAsync(teacherId);
                return Ok(teacherAssignment);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpGet]
        [Route("class/{classId:guid}")]
        public async Task<IActionResult> GetAssignmentForClass(Guid classId)
        {
            try
            {
                var classAssignment = await _subjectTeacherServices.GetAssignmentsForClassAsync(classId);
                return Ok(classAssignment);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpGet]
        [Route("subject/{subjectId:guid}")]
        public async Task<IActionResult> GetAssignmentForSubject(Guid subjectId)
        {
            try
            {
                var subjectAssignment = await _subjectTeacherServices.GetAssignmentsForSubjectAsync(subjectId);
                return Ok(subjectAssignment);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Assign([FromBody] AddSubjectTeacherDto addSubjectTeacherDto)
        {
            try
            {
                if (addSubjectTeacherDto is null)
                {
                    return BadRequest();
                }

                await _subjectTeacherServices.AssignAsync(addSubjectTeacherDto);
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

        public async Task<IActionResult> Unassign([FromBody] DeleteSubjectTeacherDto deleteSubjectTeacherDto)
        {
            try
            {
                if (deleteSubjectTeacherDto is null)
                {
                    return BadRequest();
                }

                var success = await _subjectTeacherServices.UnassignAsync(deleteSubjectTeacherDto);
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
