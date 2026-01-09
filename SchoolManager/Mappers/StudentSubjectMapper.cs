using SchoolManager.Dtos.Student;
using SchoolManager.Models.Entities;

namespace SchoolManager.Mappers
{
    public static class StudentSubjectMapper
    {
        public static StudentSubjectDto ToStudentSubjectDto(this StudentSubject studSub)
        {
            return new StudentSubjectDto()
            {
                SubjectId = studSub.SubjectId,
                SubjectName = studSub.Subject.Name
            };
        }
    }
}
