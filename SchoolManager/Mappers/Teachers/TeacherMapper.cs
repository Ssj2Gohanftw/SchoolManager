using SchoolManager.Dtos.Student;
using SchoolManager.Dtos.Teacher;
using SchoolManager.Models.Entities;
namespace SchoolManager.Mappers.Teachers
{
    public static class TeacherMapper
    {
        public static TeacherSummaryDto ToTeacherSummaryDto(this Teacher teacher )
        {
            return new TeacherSummaryDto
            {
                TeacherId = teacher.TeacherId,
                FirstName = teacher.FirstName,
                LastName = teacher.LastName,
                Email = teacher.Email
            };
        }

        public static TeacherDetailsDto ToTeacherDetailsDto(this Teacher teacher )
        {
            return new TeacherDetailsDto
            {
                TeacherId = teacher.TeacherId,
                FirstName = teacher.FirstName,
                LastName = teacher.LastName,
                Email = teacher.Email,
                Assignments = teacher.SubjectTeachers.Select(st => new TeacherAssignmentDto
                {
                    ClassId = st.ClassId,
                    ClassName = st.Class.Name,
                    SubjectId = st.SubjectId,
                    SubjectName = st.Subject.Name
                }).ToList()
            };
        }
        public static Teacher ToTeacher(this AddTeacherDto addTeacherDto)
        {
            return new Teacher
            {
                FirstName = addTeacherDto.FirstName,
                LastName = addTeacherDto.LastName,
                Email = addTeacherDto.Email
            };
        }
        public static void ToUpdateTeacher(this UpdateTeacherDto updateTeacherDto, Teacher teacher)
        {
            teacher.FirstName = updateTeacherDto.FirstName;
            teacher.LastName = updateTeacherDto.LastName;
            teacher.Email = updateTeacherDto.Email;
        }
    }
}