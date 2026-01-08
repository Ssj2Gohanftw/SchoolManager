namespace SchoolManager.Dtos.University
{
    public enum UniversitySearchOptions { 
        name,
        country,
        //[EnumMember(Value = "state-province")]
        //StateProvince
    }

    public class UniversityQueryDto
    {
            public UniversitySearchOptions? SearchOptions { get; init; }
            public string? Search { get; init; }

        //public static readonly Dictionary<UniversitySearchOptions, string> SearchOptionParams = new()
        //{
        //    {UniversitySearchOptions.name ,"name"},
        //    {UniversitySearchOptions.country ,"country"},
        //    {UniversitySearchOptions.stateProvince ,"state-province"},
        
        //};
       }

    }

