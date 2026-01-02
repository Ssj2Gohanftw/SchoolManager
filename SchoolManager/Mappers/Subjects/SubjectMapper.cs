using SchoolManager.Dtos.Subject;
using SchoolManager.Models.Entities;
namespace SchoolManager.Mappers.Subjects
{
    public static class SubjectMapper
    {
        public static SubjectSummaryDto ToSubjectSummaryDto(this Subject subject)
        {
            return new SubjectSummaryDto
            {
                SubjectId = subject.SubjectId,
                Name = subject.Name
            };
        }
    }
}