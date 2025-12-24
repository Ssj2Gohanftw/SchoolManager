using Microsoft.AspNetCore.Mvc;
using SchoolManager.Models.Dtos.Subject;
using SchoolManager.Services.Interfaces;

namespace SchoolManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")] 
    public class SubjectController : ControllerBase
    {
        private readonly ISubjectServices _subjectServices;
        public SubjectController(ISubjectServices subjectServices)
        {
            _subjectServices = subjectServices;
        }
        [HttpGet]
        public async Task <IActionResult> GetAllSubjects()
        {
            var allSubjects = await _subjectServices.GetAllAsync();
            return Ok(allSubjects);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetSubjectById(Guid id)
        {
            var subject = await _subjectServices.GetSubjectByIdAsync(id);
            if (subject is null)
            {
                return NotFound();
            }
            return Ok(subject);
        }
        [HttpPost]
        public async Task<IActionResult> AddSubject(AddSubjectDto addSubjectDto)
        {
            var subject= await _subjectServices.AddSubjectAsync(addSubjectDto);
            return Ok(subject);
        }
        [HttpPut]
        [Route("{id:guid}")]

        public async Task<IActionResult> UpdateSubject(Guid id,UpdateSubjectDto updateSubjectDto)
        {
            var success =await _subjectServices.UpdateSubjectAsync(id,updateSubjectDto);
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
    }
}
