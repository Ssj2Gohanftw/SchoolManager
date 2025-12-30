using SchoolManager.Models.Dtos.Student;
namespace SchoolManager.Models.Mappings.Student
{
    public static class StudentMappings
    {
        public static StudentDto ToStudentDto(this Entities.Student student)
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
