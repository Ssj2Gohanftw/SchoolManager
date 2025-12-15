namespace SchoolManager.Models.Dtos.Student
{
    public class UpdateStudentDto
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public DateTimeOffset DateOfBirth { get; set; }

        public required string Email { get; set; }
    }
}
