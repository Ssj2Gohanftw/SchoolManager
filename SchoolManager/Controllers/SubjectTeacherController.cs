using Microsoft.AspNetCore.Mvc;
using SchoolManager.Models.Dtos.SubjectTeacher;
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
            var teacherAssignment= await _subjectTeacherServices.GetAssignmentsForTeacherAsync(teacherId);
            return Ok(teacherAssignment);
        }
        [HttpGet]
        [Route("class/{classId:guid}")]
        public async Task<IActionResult> GetAssignmentForClass(Guid classId)
        {
            var classAssignment = await _subjectTeacherServices.GetAssignmentsForClassAsync(classId);
            return Ok(classAssignment);
        }
        [HttpGet]
        [Route("subject/{subjectId:guid}")]
        public async Task<IActionResult> GetAssignmentForSubject(Guid subjectId)
        {
            var subjectAssignment = await _subjectTeacherServices.GetAssignmentsForSubjectAsync(subjectId);
            return Ok(subjectAssignment);
        }

        [HttpPost]
        public async Task<IActionResult> Assign([FromBody] AddSubjectTeacherDto addSubjectTeacherDto)
        {
            if (addSubjectTeacherDto is null)
            {
                return BadRequest();
            }

            await _subjectTeacherServices.AssignAsync(addSubjectTeacherDto);
            return Ok();
        }

        [HttpDelete]
        
        public async Task<IActionResult> Unassign([FromBody] DeleteSubjectTeacherDto deleteSubjectTeacherDto)
        {
            if (deleteSubjectTeacherDto is null)
            {
                return BadRequest();
            }

            var success= await _subjectTeacherServices.UnassignAsync(deleteSubjectTeacherDto);
            if (!success) return NotFound();
            return Ok();
        }
    }
}
