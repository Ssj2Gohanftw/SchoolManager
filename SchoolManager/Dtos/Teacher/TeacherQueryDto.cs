using SchoolManager.Dtos.Common;

namespace SchoolManager.Dtos.Teacher
{
    public enum TeacherSortBy
    {
        FirstName,
        LastName,
        Email
    }
    public class TeacherQueryDto:QueryDto
    {
        public TeacherSortBy SortBy { get; init; } = TeacherSortBy.FirstName;
    }
}
