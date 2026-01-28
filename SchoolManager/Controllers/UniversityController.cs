using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using SchoolManager.Dtos.University;
using SchoolManager.Extensions;
using SchoolManager.Services.Interfaces;

namespace SchoolManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UniversityController : ControllerBase
    {
        private readonly IApiService _iapiService;
        private readonly IValidator<UniversityQueryDto> _validator;
        public UniversityController(IApiService iapiService, IValidator<UniversityQueryDto> validator)
        {
            _iapiService = iapiService;
            _validator = validator;
        }

        [HttpGet]
        [Route("search")]
        public async Task<IActionResult> Search([FromQuery] UniversityQueryDto universityQueryDto)
        {

            var country = universityQueryDto?.Country?.Trim();
            var name = universityQueryDto?.Name?.Trim();
            var limit = universityQueryDto?.Limit;
            var province = universityQueryDto?.StateProvince?.Trim();
            var baseUrl = "search?";


            var url = $"country={country}&name={(name)}&limit={limit}&state-province={province}";
            var validationResult = await _validator.ValidateAsync(universityQueryDto);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState);
                return ValidationProblem(ModelState);
            }
            baseUrl += url;
            //var url = QueryHelpers.AddQueryString("search", country,name);
            var data = await _iapiService.GetDataAsync<List<UniversityDto>>(baseUrl);
            return Ok(data);
        }

    }
}
