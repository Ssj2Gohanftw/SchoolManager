using Microsoft.AspNetCore.Mvc;
using SchoolManager.Dtos.Class;
using SchoolManager.Services.Interfaces;

namespace SchoolManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClassController : ControllerBase
    {
        private readonly IClassServices _classServices;
        public ClassController(IClassServices classServices)
        {
            _classServices = classServices;

        }

        [HttpGet]
        public async Task<IActionResult> GetAllClasses()
        {
            try
            {
                var classes = await _classServices.GetAllAsync();
                return Ok(classes);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetClassById(Guid id)
        {
            try
            {
                var @class = await _classServices.GetClassByIdAsync(id);
                if (@class is null)
                {
                    return NotFound();
                }
                return Ok(@class);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddClass(AddClassDto addClassDto)
        {
            try
            {
                var @class = await _classServices.AddClassAsync(addClassDto);
                return Ok(@class);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut]
        [Route("{id:guid}")]

        public async Task<IActionResult> UpdateClass(Guid id, UpdateClassDto updateClassDto)
        {
            try
            {
                var success = await _classServices.UpdateClassAsync(id, updateClassDto);
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

        public async Task<IActionResult> DeleteClass(Guid id)
        {
            try
            {
                var success = await _classServices.DeleteClassAsync(id);
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
        public async Task<IActionResult> GetClassesPaged([FromQuery] ClassQueryDto classQueryDto)
        {
            try
            {
                var result = await _classServices.GetPagedClassesAsync(classQueryDto);
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
