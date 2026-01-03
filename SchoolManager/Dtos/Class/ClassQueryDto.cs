using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SchoolManager.Dtos.Common;

namespace SchoolManager.Dtos.Class
{
    public enum ClassSortBy
    {
        Name,
        ClassId
    }
    public enum ClassFilterBy
    {
        None,
        Search
    }

    public class ClassQueryDto
    {
        public ClassFilterBy FilterBy { get; init; } = ClassFilterBy.None;
        public string? Search { get; init; }
        public ClassSortBy SortBy { get; init; } = ClassSortBy.Name;
        public SortOrder SortOrder { get; init; } = SortOrder.Ascending;

        public int PageNumber { get; init; } = 1;
        public int PageSize { get; init; } = 20;


    }
}
