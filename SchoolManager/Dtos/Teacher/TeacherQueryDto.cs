using SchoolManager.Dtos.Common;

namespace SchoolManager.Dtos.Teacher
{
    public enum TeacherSortBy
    {
        Name,
        Email
    }
    public enum TeacherFilterBy
    {
        None,
        Search
    }
    public class TeacherQueryDto
    {
        public TeacherFilterBy FilterBy { get; init; } = TeacherFilterBy.None;
        public string? Search { get; init; }
        public TeacherSortBy SortBy { get; init; } = TeacherSortBy.Name;
        public SortDirection SortDirection { get; init; } = SortDirection.Asc;

        public int PageNumber { get; init; } = 1;
        public int PageSize { get; init; } = 20;
    }
}
