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
        public static Subject ToSubject(this AddSubjectDto addSubjectDto)
        {
            return new Subject
            {
                Name = addSubjectDto.Name
            };
        }
        public static void ToUpdateSubject(this UpdateSubjectDto updateSubjectDto, Subject subject)
        {

            subject.Name = updateSubjectDto.Name;
        }
    }
}