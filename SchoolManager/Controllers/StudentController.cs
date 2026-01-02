using Microsoft.AspNetCore.Mvc;
using SchoolManager.Dtos.Student;
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
            try
            {
                var allStudents = await _studentServices.GetAllAsync();
                return Ok(allStudents);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        //[HttpGet("sorted")]
        //public async Task<IActionResult> GetAllStudentsSorted()
        //{
        //    try
        //    {
        //        var allStudents = await _studentServices.GetAllSortedAsync();
        //        return Ok(allStudents);
        //    }
        //    catch (Exception)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError);
        //    }
        //}

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetStudentById(Guid id)
        {
            try
            {
                var student = await _studentServices.GetStudentByIdAsync(id);
                if (student is null)
                {
                    return NotFound(new { message = "Student not found" });
                }
                var studentDetails = new StudentDto()
                {
                    StudentId = student.StudentId,
                    FirstName = student.FirstName,
                    LastName = student.LastName,
                    Email = student.Email,
                    DateOfBirth = student.DateOfBirth,
                    ClassId = student.ClassId,
                    ClassName = student.Class?.Name
                };

                return Ok(studentDetails);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddStudent(AddStudentDto addStudentDto)
        {
            try
            {
                var student = await _studentServices.AddStudentAsync(addStudentDto);
                if (student is null)
                {
                    return BadRequest(new { message = "A Class with that name doesn't exist so Student can't be assigned to the class" });
                }
                return Ok(student);

            }
            catch (InvalidOperationException)
            {
                return BadRequest(new { message = "A Class with that name doesn't exist so Student can't be assigned to the class" });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }
        [HttpPut]
        [Route("{id:guid}")]

        public async Task<IActionResult> UpdateStudent(Guid id, UpdateStudentDto updateStudentDto)
        {
            try
            {
                var success = await _studentServices.UpdateStudentAsync(id, updateStudentDto);
                if (!success)
                {
                    return BadRequest(new { message = "A Student with that name doesn't exist" });
                }

                return Ok();
            }
            catch (InvalidOperationException)
            {
                return BadRequest(new { message = "Class doesn't exist" });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete]
        [Route("{id:guid}")]

        public async Task<IActionResult> DeleteStudent(Guid id)
        {
            try
            {
                var success = await _studentServices.DeleteStudentAsync(id);
                if (!success)
                {
                    return BadRequest(new { message = "A Student with that name doesn't exist" });
                }
                return Ok();

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

        [HttpGet("paged")]
        public async Task<IActionResult> GetStudentsPaged([FromQuery] StudentQueryDto studentQueryDto)
        {
            try
            {
                var result = await _studentServices.GetPagedStudentsAsync(studentQueryDto);
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
