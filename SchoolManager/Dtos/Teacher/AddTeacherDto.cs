using SchoolManager.Models.Entities;

namespace SchoolManager.Dtos.Teacher
{
    public class AddTeacherDto
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
    }
}
