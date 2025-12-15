namespace SchoolManager.Models.Entities
{
    public class Teacher
    {
        public Guid TeacherId { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public ICollection<SubjectTeacher> SubjectTeacher { get; set; }
            = new List<SubjectTeacher>();

    }
}
