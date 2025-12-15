namespace SchoolManager.Models.Entities
{
    public class StudentClass
    {
        public Guid StudentId { get; set; }
        public Student Student { get; set; } = null!;
        public Guid ClassId { get; set; }
        public Class Class { get; set; } = null!;
    }
}
