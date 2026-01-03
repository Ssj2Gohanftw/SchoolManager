using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace SchoolManager.Dtos.Student
{
    public enum StudentSortBy
    {
        FirstName,
        LastName,
        Email,
        DateOfBirth,
        ClassName
    }

    public enum StudentFilterBy
    {
        None,
        Search,
        ClassId
    }

    public class StudentQueryDto
    {
        public StudentFilterBy FilterBy { get; init; } = StudentFilterBy.None;

        public string? Search { get; init; }
        public Guid? ClassId { get; init; }

        public StudentSortBy SortBy { get; init; } = StudentSortBy.FirstName;
        public SortOrder SortOrder { get; init; } = SortOrder.Ascending;
        public int PageNumber { get; init; } = 1;
        public int PageSize { get; init; } = 20;
    }
}
