using SchoolManager.Models.Dtos.SubjectTeacher;
using SchoolManager.Models.Entities;

namespace SchoolManager.Models.Mappings
{
    public static class SubjectTeacherMappings
    {
        public static SubjectTeacherDto ToSubjectTeacherDto(this SubjectTeacher subjectTeacher)
        {
            return new SubjectTeacherDto
            {
                TeacherId = subjectTeacher.TeacherId,
                TeacherName = $"{subjectTeacher.Teacher.FirstName} {subjectTeacher.Teacher.LastName}",
                ClassId = subjectTeacher.ClassId,
                ClassName = subjectTeacher.Class.Name,
                SubjectId = subjectTeacher.SubjectId,
                SubjectName = subjectTeacher.Subject.Name
            };
        }
    }
}
