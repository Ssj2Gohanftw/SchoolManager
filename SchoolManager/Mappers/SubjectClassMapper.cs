using SchoolManager.Dtos.Subject;
using SchoolManager.Dtos.SubjectClass;
using SchoolManager.Mappers.Subjects;
using SchoolManager.Models;

namespace SchoolManager.Mappers
{
    public static class SubjectClassMapper
    {
        //public static SubjectSummaryDto ToSubjectClassDto(this SubjectClass subjectClass)
        //{
        //    return new SubjectSummaryDto
        //    {
        //        //ClassId = subjectClass.ClassId,
        //        //ClassName = subjectClass.Class.Name,
        //        //SubjectId = subjectClass.SubjectId,
        //        //SubjectName = subjectClass.Subject.Name
        //        Name=subjectClass.Subject.Name,
        //        SubjectId=subjectClass.SubjectId
        //    };
        //}
        public static SubjectSummaryDto ToSubjectSummaryDto(this SubjectClass subjectClass)
        {
            return new SubjectSummaryDto
            {
                //ClassId = subjectClass.ClassId,
                //ClassName = subjectClass.Class.Name,
                //SubjectId = subjectClass.SubjectId,
                //SubjectName = subjectClass.Subject.Name
                Name=subjectClass.Subject.Name,
                SubjectId=subjectClass.SubjectId
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
        public static SubjectClass ToSubjectClass(this Guid subjectId,Guid classId)
        {
            return new SubjectClass
            {
                SubjectId = subjectId,
                ClassId = classId
            };
        }
    }
}
