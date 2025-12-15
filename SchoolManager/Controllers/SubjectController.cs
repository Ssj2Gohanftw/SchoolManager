using Microsoft.AspNetCore.Mvc;
using SchoolManager.Data;
using SchoolManager.Models.Dtos.Student;
using SchoolManager.Models.Dtos.Subject;
using SchoolManager.Models.Entities;

namespace SchoolManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")] 
    public class SubjectController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        public SubjectController(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        [HttpGet]
        public IActionResult GetAllSubjects()
        {
            var allSubjects = _dbContext.Subjects.ToList();
            return Ok(allSubjects);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult GetSubjectById(Guid id)
        {
            var subject = _dbContext.Subjects.Find(id);
            if (subject is null)
            {
                return NotFound();
            }
            return Ok(subject);
        }
        [HttpPost]
        public IActionResult AddSubject(AddSubjectDto addSubjectDto)
        {
            var subject= new Subject()
            {
                
                Name= addSubjectDto.Name,

            };
            _dbContext.Subjects.Add(subject);
            _dbContext.SaveChanges();
            return Ok(subject);
        }
        [HttpPut]
        [Route("{id:guid}")]

        public IActionResult UpdateSubject(UpdateSubjectDto updateSubjectDto, Guid id)
        {
            var subject= _dbContext.Subjects.Find(id);
            if (subject is null)
            {
                return NotFound();
            }
            subject.Name = updateSubjectDto.Name;
            _dbContext.SaveChanges();
            return Ok();
        }

        [HttpDelete]
        [Route("{id:guid}")]

        public IActionResult DeleteSubject(Guid id)
        {
            var subject= _dbContext.Subjects.Find(id);
            if (subject is null)
            {
                return NotFound();
            }
            _dbContext.Subjects.Remove(subject);
            _dbContext.SaveChanges();
            return Ok();

        }
    }
}
