using SchoolManager.Dtos.Common;

namespace SchoolManager.Dtos.Class
{
    public enum ClassSortBy
    {
        Name,
        ClassId,
        Branch
    }

    public class ClassQueryDto:QueryDto
    {
        public ClassSortBy SortBy { get; init; } = ClassSortBy.Name;
    }
}
