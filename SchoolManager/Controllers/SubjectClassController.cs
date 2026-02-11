using Microsoft.AspNetCore.Mvc;
using SchoolManager.Dtos.SubjectClass;
using SchoolManager.Services.Interfaces;

namespace SchoolManager.Controllers
{
    [ApiController]
    [Route("api/subject-class")]
    public class SubjectClassController:ControllerBase
    {
        private readonly ISubjectClassServices _subjectClassServices;
        public SubjectClassController(ISubjectClassServices subjectClassServices)
        {
            _subjectClassServices = subjectClassServices;
        }

        [HttpPost]
        [Route("assignments")]
        public async Task<IActionResult> AssignSubject(AddSubjectClassDto addSubjectClassDto) 
        {
             var assignments= await _subjectClassServices.AssignSubjects(addSubjectClassDto);
            return Ok(assignments);

        }
        [HttpGet]
        [Route("assignments/{id:guid}/list")]
        public async Task<IActionResult> GetAssignments(Guid id)
        {
            var assignments = await _subjectClassServices.GetAssignmentDetailsForClassAsync(id);
            return Ok(assignments);

        }

    }
}
