using SchoolManager.Dtos.Common;

namespace SchoolManager.Dtos.Class
{
    public enum ClassSortBy
    {
        Name,
        ClassId
    }

    public class ClassQueryDto:QueryDto
    {
        public ClassSortBy SortBy { get; init; } = ClassSortBy.Name;
    }
}
