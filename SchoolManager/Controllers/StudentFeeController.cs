using Microsoft.AspNetCore.Mvc;
using SchoolManager.Dtos.StudentFee;
using SchoolManager.Services.Interfaces;

namespace SchoolManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentFeeController : ControllerBase
    {
        private readonly IStudentFeeServices _studentFeeServices;
        public StudentFeeController(IStudentFeeServices studentFeeServices)
        {
            _studentFeeServices = studentFeeServices;
        }
        [HttpGet]
        [Route("{studentId:guid}")]
        public async Task<IActionResult> GetStudentFeesbyStudentId(Guid studentId)
        {
            var studentFees = await _studentFeeServices.GetFeesByStudentIdAsync(studentId);
            if (studentFees == null || !studentFees.Any())
            {
                return NotFound();
            }
            return Ok(studentFees);
        }
        [HttpPut]
        [Route("{studentId:guid}/pay")]
        public async Task<IActionResult> PayFees(
            Guid studentId, UpdateStudentFeeDto updateStudentFeeDto
            )
        {
            var studentFees = await _studentFeeServices.PayFeesAsync(studentId,updateStudentFeeDto);
            if (studentFees == null)
            {
                return NotFound();
            }
            return Ok(studentFees);
        }
    }
}
