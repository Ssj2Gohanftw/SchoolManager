using Microsoft.AspNetCore.Mvc;
using SchoolManager.Dtos.University;
using SchoolManager.Services.Interfaces;

namespace SchoolManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UniversityController : ControllerBase
    {
        private readonly IApiService _iapiService;
        public UniversityController(IApiService iapiService)
        {
            _iapiService = iapiService;            
        }

        [HttpGet]
        [Route("search")]
        public async Task<IActionResult> Search([FromQuery] string? country)
        {
            var url = $"search?country={Uri.EscapeDataString(country)}";
            var data = await _iapiService.GetDataAsync<List<UniversityDto>>(url);
            return Ok(data);
        }

    }
}
