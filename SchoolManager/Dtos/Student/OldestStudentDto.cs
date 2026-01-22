
namespace SchoolManager.Dtos.Student
{
    public class OldestStudentDto
    {
        public Guid StudentId { get; set; }
        public required string Name { get; set; }
        public int Age { get; set; }
        public required string ClassName{ get; set; }

    }
}
