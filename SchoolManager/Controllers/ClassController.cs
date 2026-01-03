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
              var classes = await _classServices.GetAllAsync();
            return Ok(classes);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetClassById(Guid id)
        {
            var @class = await _classServices.GetClassByIdAsync(id);
                if (@class is null)
                {
                    return NotFound();
                }
                return Ok(@class);
        }

        [HttpPost]
        public async Task<IActionResult> AddClass(AddClassDto addClassDto)
        {
              var @class = await _classServices.AddClassAsync(addClassDto);
                return Ok(@class);
        }

        [HttpPut]
        [Route("{id:guid}")]

        public async Task<IActionResult> UpdateClass(Guid id, UpdateClassDto updateClassDto)
        {   
                var success = await _classServices.UpdateClassAsync(id, updateClassDto);
                if (!success)
                {
                    return NotFound();
                }
                return Ok();
        }

        [HttpDelete]
        [Route("{id:guid}")]

        public async Task<IActionResult> DeleteClass(Guid id)
        {
            var success = await _classServices.DeleteClassAsync(id);
            if (!success)
                {
                    return NotFound();
                }
            return Ok();

            }
        [HttpGet("list")]
        public async Task<IActionResult> GetClassesPaged([FromQuery] ClassQueryDto classQueryDto)
        {

            var result = await _classServices.GetPagedClassesAsync(classQueryDto);
            return Ok(result);
        }
    }
}
