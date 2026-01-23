using SchoolManager.Dtos.Common;

namespace SchoolManager.Dtos.Student
{
    public enum StudentSortBy
    {
        FirstName,
        LastName,
        Email,
        DateOfBirth,
        ClassName,
        Gender
    }

    public class StudentQueryDto:QueryDto
    {
        public StudentSortBy SortBy { get; init; } = StudentSortBy.FirstName;
    }
}
