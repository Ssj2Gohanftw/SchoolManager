namespace SchoolManager.Dtos.Student
{
    public class AddStudentDto
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public DateOnly DateOfBirth { get; set; }

        public required string Email { get; set; }
        public required string ClassName { get; set; }
    }
}

