using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace SchoolManager.Dtos.University
{
    public class UniversityDto
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("domains")]
        public List<string>? Domains { get; set; }

        [JsonPropertyName("country")]
        public string? Country { get; set; }

        [JsonPropertyName("alpha_two_code")]
        public string? AlphaTwoCode { get; set; }

        [JsonPropertyName("web_pages")]
        public List<string>? WebPages { get; set; }

        [FromQuery(Name = "state-province")]
        public string? StateProvince { get; set; }
    }
}
