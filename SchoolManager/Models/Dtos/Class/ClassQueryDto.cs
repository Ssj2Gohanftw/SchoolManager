using SchoolManager.Models.Dtos.Common;

namespace SchoolManager.Models.Dtos.Class
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
        public SortDirection SortDirection { get; init; } = SortDirection.Asc;

        public int PageNumber { get; init; } = 1;
        public int PageSize { get; init; } = 20;


    }
}
