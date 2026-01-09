using SchoolManager.Dtos.Class;
using SchoolManager.Models.Entities;
namespace SchoolManager.Mappers.Classes
{
    public static class ClassMapper
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

        public static ClassesDto ToClassesDto(this Class _class)
        {
            return new ClassesDto
            {
                ClassId = _class.ClassId,
                Name = _class.Name,
                //Students = _class.Students.Select(s => s.ToStudentClassDto()).ToList()
            };
        }
        public static ClassDetailsDto ToClassDetailsDto(this Class _class)
        {
            return new ClassDetailsDto
            {
                ClassId = _class.ClassId,
                Name = _class.Name,
                Students = _class.Students.Select(s => s.ToStudentClassDto()).ToList()
            };
        }
    }
}
