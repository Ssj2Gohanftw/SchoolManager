using Microsoft.AspNetCore.Mvc;
using SchoolManager.Dtos.Teacher;
using SchoolManager.Services.Interfaces;

namespace SchoolManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherServices _teacherServices;
        public TeacherController(ITeacherServices teacherServices)
        {
            _teacherServices = teacherServices;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllTeachers()
        {
            try
            {
                var allTeachers = await _teacherServices.GetAllAsync();
                return Ok(allTeachers);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetTeacherById(Guid id)
        {
            try
            {
                var teachers = await _teacherServices.GetTeacherByIdAsync(id);
                if (teachers is null)
                {
                    return NotFound();
                }
                return Ok(teachers);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddTeacher(AddTeacherDto addTeacherDto)
        {
            try
            {
                var teachers = await _teacherServices.AddTeacherAsync(addTeacherDto);
                return Ok(teachers);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpPut]
        [Route("{id:guid}")]

        public async Task<IActionResult> UpdateTeacher(UpdateTeacherDto updateTeacherDto, Guid id)
        {
            try
            {
                var success = await _teacherServices.UpdateTeacherAsync(id, updateTeacherDto);
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

        public async Task<IActionResult> DeleteTeacher(Guid id)
        {
            try
            {
                var success = await _teacherServices.DeleteTeacherAsync(id);
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
        [HttpGet("{id:guid}/details")]
        public async Task<IActionResult> GetTeacherDetailsById(Guid id)
        {
            try
            {
                var teacher = await _teacherServices.GetTeacherDetailsByIdAsync(id);
                if (teacher is null)
                {
                    return NotFound();
                }

                return Ok(teacher);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("paged")]
        public async Task<IActionResult> GetPagedResults([FromQuery] TeacherQueryDto teacherQueryDto)
        {
            try
            {
                var pagedResults = await _teacherServices.GetPagedTeachersAsync(teacherQueryDto);
                return Ok(pagedResults);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
