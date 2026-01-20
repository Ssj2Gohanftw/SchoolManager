using SchoolManager.Dtos.SubjectTeacher;
using SchoolManager.Models;

namespace SchoolManager.Mappers
{
    public static class SubjectTeacherMapper
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
        public static SubjectTeacher ToSubjectTeacher(this AddSubjectTeacherDto addSubjectTeacherDto)
        {
            return new SubjectTeacher
            {
                TeacherId = addSubjectTeacherDto.TeacherId,
                ClassId = addSubjectTeacherDto.ClassId,
                SubjectId = addSubjectTeacherDto.SubjectId,
            };
        }
        public static SubjectTeacher ToUnassignSubjectTeacher(this DeleteSubjectTeacherDto deleteSubjectTeacherDto)
        {
            return new SubjectTeacher
            {
                TeacherId = deleteSubjectTeacherDto.TeacherId,
                ClassId = deleteSubjectTeacherDto.ClassId,
                SubjectId = deleteSubjectTeacherDto.SubjectId,
            };
        }

    }
}
