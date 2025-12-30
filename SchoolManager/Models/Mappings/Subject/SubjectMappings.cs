using SchoolManager.Models.Dtos.Subject;


namespace SchoolManager.Models.Mappings.Subject
{
    public static class SubjectMappings
    {
        public static SubjectSummaryDto ToSubjectSummaryDto(this Entities.Subject subject)
        {
            return new SubjectSummaryDto
            {
                SubjectId = subject.SubjectId,
                Name = subject.Name
            };
        }
    }
}