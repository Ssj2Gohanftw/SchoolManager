using Microsoft.AspNetCore.Mvc;
using SchoolManager.Dtos.University;
using SchoolManager.Services.Interfaces;
using Microsoft.AspNetCore.WebUtilities;

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

        //[HttpGet]
        //[Route("search")]
        //public async Task<IActionResult> Search([FromQuery] string? country)
        //{
        //    var url = $"search?country={Uri.EscapeDataString(country)}";
        //    var data = await _iapiService.GetDataAsync<List<UniversityDto>>(url);
        //    return Ok(data);
        //}
        [HttpGet]
        [Route("search")]
        public async Task<IActionResult> Search([FromQuery] UniversityQueryDto universityQueryDto)
        {
            if (!universityQueryDto.SearchOptions.HasValue)
            {
                return BadRequest("Choose a valid search option!");
            }
            if (string.IsNullOrWhiteSpace(universityQueryDto.Search))
            {
                return BadRequest("Provide a valid search term!");
            }
            var key = universityQueryDto.SearchOptions.Value.ToString();
           
            var s = universityQueryDto.Search;
            //var url = $"search?{key}={Uri.EscapeDataString(s)}";
            var url = QueryHelpers.AddQueryString("search", key, s);
            var data = await _iapiService.GetDataAsync<List<UniversityDto>>(url);
            return Ok(data);
        }       

    }
}
