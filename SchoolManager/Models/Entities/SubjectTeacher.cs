namespace SchoolManager.Models.Entities
{
    public class SubjectTeacher
    {
        public Guid TeacherId { get; set; }
        public Teacher Teacher { get; set; } = null!;
        public Guid ClassId { get; set; }
        public Class Class { get; set; } = null!;
        public Guid SubjectId { get; set; }
        public Subject Subject { get; set; } = null!;

    }
}
