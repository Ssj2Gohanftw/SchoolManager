using Microsoft.AspNetCore.Mvc;
using SchoolManager.Data;
using SchoolManager.Models.Dtos.Teacher;
using SchoolManager.Models.Entities;

namespace SchoolManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeacherController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        public TeacherController(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        [HttpGet]
        public IActionResult GetAllTeachers()
        {
            var allTeachers = _dbContext.Teachers.ToList();
            return Ok(allTeachers);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult GetTeacherById(Guid id)
        {
            var teachers = _dbContext.Teachers.Find(id);
            if (teachers is null)
            {
                return NotFound();
            }
            return Ok(teachers);
        }
        [HttpPost]
        public IActionResult AddTeacher(AddTeacherDto addTeacherDto)
        {
            var teachers = new Teacher()
            {
                FirstName = addTeacherDto.FirstName,
                LastName = addTeacherDto.LastName,
                Email = addTeacherDto.Email                
            };
            _dbContext.Teachers.Add(teachers);
            _dbContext.SaveChanges();
            return Ok(teachers);
        }
        [HttpPut]
        [Route("{id:guid}")]

        public IActionResult UpdateTeacher(UpdateTeacherDto updateTeacherDto, Guid id)
        {
            var teachers = _dbContext.Teachers.Find(id);
            if (teachers is null)
            {
                return NotFound();
            }
            teachers.FirstName = updateTeacherDto.FirstName;
            teachers.LastName = updateTeacherDto.LastName;
            teachers.Email = updateTeacherDto.Email;
            _dbContext.SaveChanges();
            return Ok();
        }

        [HttpDelete]
        [Route("{id:guid}")]

        public IActionResult DeleteTeacher(Guid id)
        {
            var teachers = _dbContext.Teachers.Find(id);
            if (teachers is null)
            {
                return NotFound();
            }
            _dbContext.Teachers.Remove(teachers);
            _dbContext.SaveChanges();
            return Ok();

        }
    }
}
