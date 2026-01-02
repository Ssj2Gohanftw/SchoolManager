namespace SchoolManager.Dtos.Teacher
{
    public class TeacherDetailsDto
    {
        public Guid TeacherId { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }

        public List<TeacherAssignmentDto> Assignments { get; set; } = new();
    }
}