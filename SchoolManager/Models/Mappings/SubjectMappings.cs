using SchoolManager.Models.Dtos.Subject;
using SchoolManager.Models.Entities;

namespace SchoolManager.Models.Mappings
{
    public static class SubjectMappings
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