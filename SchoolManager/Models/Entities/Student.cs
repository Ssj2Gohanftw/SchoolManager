namespace SchoolManager.Models.Entities
{
    public class Student
    {
        public Guid StudentId { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public DateTimeOffset DateOfBirth { get; set; }

        public required string Email { get; set; }
        public ICollection<StudentClass> StudentClasses { get; set; }
            = new List<StudentClass>(); 
    }
}
