using SchoolManager.Dtos.Common;


namespace SchoolManager.Dtos.Subject
{
    public enum SubjectSortBy
    {
        Name,
        SubjectId
    }
    public class SubjectQueryDto:QueryDto
    {
        public SubjectSortBy SortBy { get; init; } = SubjectSortBy.Name;
    }
}
