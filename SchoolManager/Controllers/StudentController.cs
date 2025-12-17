using Microsoft.AspNetCore.Mvc;
using SchoolManager.Models.Dtos.Student;
using SchoolManager.Services.Interfaces;

namespace SchoolManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController:ControllerBase
    {
        private readonly IStudentServices _studentServices;
        
        public StudentController(IStudentServices studentServices)
        {
            _studentServices = studentServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStudents(CancellationToken cancellationToken)
        {
            var allStudents = await _studentServices.GetAllAsync(cancellationToken);
            return Ok(allStudents);
            
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task <IActionResult>GetStudentById(Guid id ,CancellationToken cancellationToken)
        {
            var student = await _studentServices.GetStudentByIdAsync(id,cancellationToken);
            if(student is null)
            {
                return NotFound();
            }
            return Ok(student);
        }
        [HttpPost]
        public async Task<IActionResult> AddStudent(AddStudentDto addStudentDto,CancellationToken cancellationToken)
        {
            var student = await _studentServices.AddStudentAsync(addStudentDto,cancellationToken);
            return Ok(student);
            
        }
        [HttpPut]
        [Route("{id:guid}")]

        public async Task <IActionResult> UpdateStudent(Guid id, UpdateStudentDto updateStudentDto,CancellationToken cancellationToken)
        {
            var success = await _studentServices.UpdateStudentAsync(id,updateStudentDto,cancellationToken);
            if (!success)
            {
                return NotFound();
            }
            
            return Ok();
        }

        [HttpDelete]
        [Route("{id:guid}")]

        public async Task <IActionResult> DeleteStudent(Guid id,CancellationToken cancellationToken)
        {
            var success = await _studentServices.DeleteStudentAsync(id,cancellationToken);
            if (!success )
            {
                return NotFound();
            }
            return Ok();

        }
    }
}
