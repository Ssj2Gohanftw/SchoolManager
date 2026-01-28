using System.ComponentModel.DataAnnotations;

namespace SchoolManager.Models
{
    public class StudentMetaData
    {
        public string? EmergencyContact { get; set; }
        public List<string>? Hobbies { get; set; }
        public List<string>? ExtraCurriculars { get; set; }
    }
    public class Student
    {
        public Guid StudentId { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        
        public DateOnly DateOfBirth { get; set; }
        public required string Gender { get; set; }
        [EmailAddress]
        public required string Email { get; set; }

        public Guid? ClassId { get; set; }
        public Class? Class { get; set; }
        public List<StudentSubject> StudentSubjects { get; set; } = new();
        //public List<SubjectClass> Subjects { get; set; } = new();
        public StudentMetaData? AdditionalInfo { get; set; }

        public List<StudentFee> StudentFees { get; set; } = new List<StudentFee>();
    }
}
