using Microsoft.AspNetCore.Mvc;
using SchoolManager.Dtos.Student;
using SchoolManager.Mappers.Students;
using SchoolManager.Services.Interfaces;

namespace SchoolManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentServices _studentServices;

        public StudentController(IStudentServices studentServices)
        {
            _studentServices = studentServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStudents()
        {

            var allStudents = await _studentServices.GetAllAsync();
            return Ok(allStudents);
        }

    

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetStudentById(Guid id)
        {
            
                var student = await _studentServices.GetStudentByIdAsync(id);
                if (student is null)
                {
                    return NotFound(new { message = "Student not found" });
                }
                var studentDetails = student.ToStudentDetailsDto(); 
                return Ok(studentDetails);
        }
        [HttpPost]
        public async Task<IActionResult> AddStudent(AddStudentDto addStudentDto)
        {

                var student = await _studentServices.AddStudentAsync(addStudentDto);
                if (student is null)
                {
                    return BadRequest(new { message = "A Class with that name doesn't exist so Student can't be assigned to the class" });
                }
                return Ok(student);

            }
        [HttpPut]
        [Route("{id:guid}")]

        public async Task<IActionResult> UpdateStudent(Guid id, UpdateStudentDto updateStudentDto)
        {    
            var success = await _studentServices.UpdateStudentAsync(id, updateStudentDto);
                if (!success)
                {
                    return BadRequest(new { message = "A Student with that name doesn't exist" });
                }

                return Ok();
            }

        [HttpDelete]
        [Route("{id:guid}")]

        public async Task<IActionResult> DeleteStudent(Guid id)
        {
                var success = await _studentServices.DeleteStudentAsync(id);
                if (!success)
                {
                    return BadRequest(new { message = "A Student with that name doesn't exist" });
                }
                return Ok();

        }
        [HttpGet("list")]
        public async Task<IActionResult> GetStudentsPaged([FromQuery] StudentQueryDto studentQueryDto)
        {
                var result = await _studentServices.GetPagedStudentsAsync(studentQueryDto);
                return Ok(result);
        }
    }
}
