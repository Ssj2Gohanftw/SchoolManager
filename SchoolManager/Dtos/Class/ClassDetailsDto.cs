using SchoolManager.Dtos.StudentClass;
using SchoolManager.Models;

namespace SchoolManager.Dtos.Class
{
    //DTO for returning a class with specific student details in it such as their id,first and last name

    public class ClassDetailsDto
    {
        public Guid ClassId { get; set; }
        public string Name { get; set; } = null!;
        public Branch Branch { get; set; }
        public List<StudentClassDto> Students { get; set; } = new();
        public List<string>? Subjects { get; set; }
    }
}
