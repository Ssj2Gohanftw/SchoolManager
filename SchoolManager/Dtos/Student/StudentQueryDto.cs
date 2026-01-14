using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SchoolManager.Dtos.Common;

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


    //public class StudentQueryDto
    //{
    //    public FilterBy FilterBy { get; init; } = FilterBy.None;

    //    public string? Search { get; init; }

    //    public StudentSortBy SortBy { get; init; } = StudentSortBy.FirstName;
    //    public SortOrder SortOrder { get; init; } = SortOrder.Ascending;
    //    public int PageNumber { get; init; } = 1;
    //    public int PageSize { get; init; } = 20;
    //}
    public class StudentQueryDto:QueryDto
    {
        public StudentSortBy SortBy { get; init; } = StudentSortBy.FirstName;
    }
}
