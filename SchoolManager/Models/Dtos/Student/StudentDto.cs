namespace SchoolManager.Models.Dtos.Student
{
    public class StudentDto
    {
        public Guid StudentId { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public required string Email { get; set; }

        public Guid? ClassId { get; set; }
        public string? ClassName { get; set; }

        public List<StudentSubjectDto> Subjects { get; set; } = new();
    }
}
