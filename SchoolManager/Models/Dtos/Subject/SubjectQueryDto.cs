using SchoolManager.Models.Dtos.Common;

namespace SchoolManager.Models.Dtos.Subject
{
    public enum SubjectSortBy
    {
        Name,
        SubjectId
    }
    public enum SubjectFilterBy
    {
        None,
        Search
    }
    public class SubjectQueryDto
    {
        public SubjectFilterBy FilterBy { get; init; } = SubjectFilterBy.None;
        public string? Search { get; init; }
        public SubjectSortBy SortBy { get; init; } = SubjectSortBy.Name;
        public SortDirection SortDirection { get; init; } = SortDirection.Asc;

        public int PageNumber { get; init; } = 1;
        public int PageSize { get; init; } = 20;
    }
}
