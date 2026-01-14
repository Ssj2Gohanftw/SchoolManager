using SchoolManager.Dtos.Common;

namespace SchoolManager.Dtos.Teacher
{
    public enum TeacherSortBy
    {
        Name,
        Email
    }
    public class TeacherQueryDto:QueryDto
    {
        public TeacherSortBy SortBy { get; init; } = TeacherSortBy.Name;
    }
}
