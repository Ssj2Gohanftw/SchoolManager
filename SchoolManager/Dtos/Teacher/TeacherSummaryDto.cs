using System.Text.Json.Serialization;

namespace SchoolManager.Dtos.Teacher
{
    public class TeacherSummaryDto
    {
        public Guid TeacherId { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
       
        [JsonIgnore]
        public string Name => $"{FirstName} {LastName}";
    }
}