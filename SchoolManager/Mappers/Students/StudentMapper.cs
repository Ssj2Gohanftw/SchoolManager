using SchoolManager.Dtos.Student;
using SchoolManager.Models.Entities;

namespace SchoolManager.Mappers.Students
{
    public static class StudentMapper
    {
        public static StudentDto ToStudentDto(this Student student )
        {
            return new StudentDto()
            {
                StudentId = student.StudentId,
                FirstName = student.FirstName,
                LastName = student.LastName,
                Email = student.Email,
                DateOfBirth = student.DateOfBirth,
                ClassId = student.ClassId,
                ClassName = student.Class?.Name
            };

        }

    }
}
