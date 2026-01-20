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
        public async Task<ActionResult<List<SubjectClassDto>>> AssignSubject(AddSubjectClassDto addSubjectClassDto) 
        {
            return await _subjectClassServices.AssignSubjects(addSubjectClassDto);
        }
    }
}
