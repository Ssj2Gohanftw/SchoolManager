using Microsoft.AspNetCore.Mvc;
using SchoolManager.Data;
using SchoolManager.Models.Dtos.Student;
using SchoolManager.Models.Entities;

namespace SchoolManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController:ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        
        public StudentController(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetAllStudents()
        {
            var allStudents = _dbContext.Students.ToList();
            return Ok(allStudents);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult GetStudentById(Guid id)
        {
            var student = _dbContext.Students.Find(id);
            if(student is null)
            {
                return NotFound();
            }
            return Ok(student);
        }
        [HttpPost]
        public IActionResult AddStudent(AddStudentDto addStudentDto)
        {
            var student = new Student()
            {
                FirstName = addStudentDto.FirstName,
                LastName = addStudentDto.LastName,
                Email=addStudentDto.Email,
                DateOfBirth = addStudentDto.DateOfBirth.ToUniversalTime()

            };
            _dbContext.Students.Add(student);
            _dbContext.SaveChanges();
            return Ok(student);
        }
        [HttpPut]
        [Route("{id:guid}")]

        public IActionResult UpdateStudent(UpdateStudentDto updateStudentDto,Guid id)
        {
            var student = _dbContext.Students.Find(id);
            if (student is null)
            {
                return NotFound();
            }
            student.FirstName = updateStudentDto.FirstName;
            student.LastName = updateStudentDto.LastName;
            student.DateOfBirth = updateStudentDto.DateOfBirth;
            student.Email = updateStudentDto.Email;
            _dbContext.SaveChanges();
            return Ok();
        }

        [HttpDelete]
        [Route("{id:guid}")]

        public IActionResult DeleteStudent(Guid id)
        {
            var student = _dbContext.Students.Find(id);
            if (student is null)
            {
                return NotFound();
            }
            _dbContext.Students.Remove(student);
            _dbContext.SaveChanges();
            return Ok();

        }
    }
}
