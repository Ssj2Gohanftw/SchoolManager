using Microsoft.AspNetCore.Mvc;
using SchoolManager.Data;
using SchoolManager.Models.Dtos.Class;
using SchoolManager.Models.Dtos.Student;
using SchoolManager.Models.Entities;

namespace SchoolManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClassController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        public ClassController(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
            
        }
        [HttpGet]
        public IActionResult GetAllClasses()
        {
            var classes = _dbContext.Class.ToList();
            return Ok(classes);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult GetClassById(Guid id)
        {
            var classes= _dbContext.Class.Find(id);
            if (classes is null)
            {
                return NotFound();
            }
            return Ok(classes);
        }
        [HttpPost]
        public IActionResult AddClass(AddClassDto addClassDto)
        {
            var classes = new Class()
            {
                Name = addClassDto.Name                
            };
            _dbContext.Class.Add(classes);
            _dbContext.SaveChanges();
            return Ok(classes);
        }
        [HttpPut]
        [Route("{id:guid}")]

        public IActionResult UpdateClass(UpdateClassDto updateClassDto, Guid id)
        {
            var classes= _dbContext.Class.Find(id);
            if (classes is null)
            {
                return NotFound();
            }
            classes.Name = updateClassDto.Name;
            _dbContext.SaveChanges();
            return Ok();
        }

        [HttpDelete]
        [Route("{id:guid}")]

        public IActionResult DeleteClass(Guid id)
        {
            var classes= _dbContext.Class.Find(id);
            if (classes is null)
            {
                return NotFound();
            }
            _dbContext.Class.Remove(classes);
            _dbContext.SaveChanges();
            return Ok();

        }
    }
}
