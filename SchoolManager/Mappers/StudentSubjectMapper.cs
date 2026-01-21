using SchoolManager.Dtos.StudentSubject;
using SchoolManager.Models;

namespace SchoolManager.Mappers
{
    public static class StudentSubjectMapper
    {
        //public static StudentSubjectDto ToStudentSubjectDto(this StudentSubject studSub)
        //{
        //    return new StudentSubjectDto()
        //    {
        //        SubjectId = studSub.SubjectId,
        //        SubjectName = studSub.Subject.Name
        //    };
        //}
        public static StudentSubjectDto ToStudentSubjectDto(this SubjectClass subjectClass)
        {
            return new StudentSubjectDto()
            {

                SubjectId = subjectClass.SubjectId,
                SubjectName = subjectClass.Subject.Name
            };
        }
    }
}
