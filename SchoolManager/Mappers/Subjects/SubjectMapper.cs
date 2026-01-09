using SchoolManager.Dtos.Subject;
using SchoolManager.Mappers.Classes;
using SchoolManager.Mappers.Teachers;
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
        public static SubjectDetailsDto ToSubjectDetailsDto(this Subject subject)
        {
            return new SubjectDetailsDto
            {
                SubjectId = subject.SubjectId,
                Name = subject.Name,
                Classes=subject.SubjectTeachers.Select(c=>c.Class.ToClassesDto()).ToList(),
                Teachers=subject.SubjectTeachers.Select(t=>t.Teacher.ToTeacherDto()).ToList()
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