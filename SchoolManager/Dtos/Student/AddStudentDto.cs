using SchoolManager.Models;

namespace SchoolManager.Dtos.Student
{
    public class AddStudentDto
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required DateOnly DateOfBirth { get; set; }
        public required string Gender { get; set;}
        public required string Email { get; set; }
        //public string ClassName { get; set; }
        public StudentMetaData? AdditionalInfo { get; set; }

    }
}

