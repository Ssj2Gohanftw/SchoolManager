using Microsoft.AspNetCore.Mvc;
namespace SchoolManager.Dtos.University
{

    public class UniversityQueryDto
    {

        [FromQuery(Name = "name")]
        public string? Name { get; init; }

        [FromQuery(Name = "country")]
        public string? Country { get; init; }

        [FromQuery(Name = "limit")]
        public int? Limit { get; init; }

        [FromQuery(Name ="state-province")]
        public string? StateProvince { get; init; }

    }
}

