using Microsoft.AspNetCore.Mvc;
using SchoolManager.Dtos.Subject;
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
            try
            {
                var allSubjects = await _subjectServices.GetAllAsync();
                return Ok(allSubjects);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetSubjectById(Guid id)
        {
            try
            {
                var subject = await _subjectServices.GetSubjectByIdAsync(id);
                if (subject is null)
                {
                    return NotFound();
                }
                return Ok(subject);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddSubject(AddSubjectDto addSubjectDto)
        {
            try
            {
                var subject = await _subjectServices.AddSubjectAsync(addSubjectDto);
                return Ok(subject);
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
        [HttpPut]
        [Route("{id:guid}")]

        public async Task<IActionResult> UpdateSubject(Guid id,UpdateSubjectDto updateSubjectDto)
        {
            try
            {
                var success =await _subjectServices.UpdateSubjectAsync(id,updateSubjectDto);
                if (!success)
                {
                    return NotFound();
                }
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete]
        [Route("{id:guid}")]

        public async Task<IActionResult> DeleteSubject(Guid id)
        {
            try
            {
                var success = await _subjectServices.DeleteSubjectAsync(id);
                if (!success)
                {
                    return NotFound();
                }
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpGet("paged")]
        public async Task<IActionResult> GetPagedSubjectsAsync([FromQuery] SubjectQueryDto subjectQueryDto)
        {
            try
            {
                var result = await _subjectServices.GetPagedSubjectsAsync(subjectQueryDto);
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
