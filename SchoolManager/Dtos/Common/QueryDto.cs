using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace SchoolManager.Dtos.Common
{
    public enum FilterBy
    {
        None,
        Search
    }
    
    public abstract class QueryDto
    {
        public FilterBy FilterBy { get; init; } = FilterBy.None;
        public string? Search { get; init; }
        public SortOrder SortOrder { get; init; } = SortOrder.Ascending;
        public int PageNumber { get; init; } = 1;
        public int PageSize { get; init; } = 20;
    }
}
