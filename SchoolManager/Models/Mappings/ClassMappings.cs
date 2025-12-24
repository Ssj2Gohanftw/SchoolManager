using SchoolManager.Models.Dtos.Class;
using SchoolManager.Models.Entities;

namespace SchoolManager.Models.Mappings
{
    public static class ClassMappings
    {
        public static StudentClassDto ToStudentClassDto(this Student student)
        {
            return new StudentClassDto
            {
                StudentId = student.StudentId,
                FirstName = student.FirstName,
                LastName = student.LastName
            };
        }

        public static ClassesDto ToClassDto(this Class @class)
        {
            return new ClassesDto
            {
                ClassId = @class.ClassId,
                Name = @class.Name,
                //Students = @class.Students.Select(s => s.ToStudentClassDto()).ToList()
            };
        }
        public static ClassDetailsDto ToClassDetailsDto(this Class @class)
        {
            return new ClassDetailsDto
            {
                ClassId = @class.ClassId,
                Name = @class.Name,
                Students = @class.Students.Select(s => s.ToStudentClassDto()).ToList()
            };
        }
    }
}
