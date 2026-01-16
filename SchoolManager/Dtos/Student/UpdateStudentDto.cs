using SchoolManager.Models.Entities;

namespace SchoolManager.Dtos.Student
{
    public class UpdateStudentDto
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateOnly? DateOfBirth { get; set; }
        public string? Email { get; set; }
        public StudentMetaData? AdditionalInfo { get; set; }
    }
}
