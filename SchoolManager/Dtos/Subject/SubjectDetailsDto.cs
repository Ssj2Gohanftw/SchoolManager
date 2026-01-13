using SchoolManager.Dtos.Class;
using SchoolManager.Dtos.Teacher;

namespace SchoolManager.Dtos.Subject
{
    public class SubjectDetailsDto
    {
        public Guid SubjectId { get; set; }
        public required string Name { get; set; }

        public List<TeacherDto>? Teachers { get; set; }
        public List<ClassesDto>? Classes { get; set; }
    }
}
