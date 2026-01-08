using SchoolManager.Models.Entities;

namespace SchoolManager.Dtos.Student
{
    public class UpdateStudentDto
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public DateOnly DateOfBirth { get; set; }

        public required string Email { get; set; }
        public string? ClassName { get; set; }
        public StudentMetaData? AdditionalInfo { get; set; }
    }
}
