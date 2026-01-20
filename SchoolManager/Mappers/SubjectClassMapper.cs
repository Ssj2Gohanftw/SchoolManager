using SchoolManager.Dtos.SubjectClass;
using SchoolManager.Models;

namespace SchoolManager.Mappers
{
    public static class SubjectClassMapper
    {
        public static SubjectClassDto ToSubjectClassDto(this SubjectClass subjectClass)
        {
            return new SubjectClassDto
            {
                ClassId = subjectClass.ClassId,
                ClassName = subjectClass.Class.Name,
                SubjectId = subjectClass.SubjectId,
                SubjectName = subjectClass.Subject.Name
            };
        }
        public static SubjectClass ToSubjectClass(this SubjectClass subjectClass)
        {
            return new SubjectClass
            {
                ClassId = subjectClass.ClassId,
                SubjectId = subjectClass.SubjectId,
            };
        }
    }
}
