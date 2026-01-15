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
        public async Task<IActionResult> Search([FromQuery] UniversityQueryDto universityQueryDto)
        {

            var country = universityQueryDto?.Country?.Trim();
            var name = universityQueryDto?.Name?.Trim();
            var limit = universityQueryDto?.Limit;
            var baseUrl = "search?";


            var url = $"country={country}&name={(name)}&limit={limit}";

            if ((string.IsNullOrEmpty(country)) && (string.IsNullOrEmpty(name)))
            {
                return BadRequest();

            }
            baseUrl += url;
            //var url = QueryHelpers.AddQueryString("search", country,name);
            var data = await _iapiService.GetDataAsync<List<UniversityDto>>(baseUrl);
            return Ok(data);
        }

    }
}
