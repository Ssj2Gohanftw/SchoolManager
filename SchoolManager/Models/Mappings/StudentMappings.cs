using SchoolManager.Models.Dtos.Student;
using SchoolManager.Models.Entities;
namespace SchoolManager.Models.Mappings
{
    public static class StudentMappings
    {
        public static StudentDto ToStudentDto(this Student student)
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
