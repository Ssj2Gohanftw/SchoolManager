using SchoolManager.Models.Dtos.Class;

namespace SchoolManager.Models.Mappings.Class
{
    public static class ClassMappings
    {
        public static StudentClassDto ToStudentClassDto(this Entities.Student student)
        {
            return new StudentClassDto
            {
                StudentId = student.StudentId,
                FirstName = student.FirstName,
                LastName = student.LastName
            };
        }

        public static ClassesDto ToClassDto(this Entities.Class @class)
        {
            return new ClassesDto
            {
                ClassId = @class.ClassId,
                Name = @class.Name,
                //Students = @class.Students.Select(s => s.ToStudentClassDto()).ToList()
            };
        }
        public static ClassDetailsDto ToClassDetailsDto(this Entities.Class @class)
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
