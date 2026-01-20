namespace SchoolManager.Models
{
    public class SubjectClass
    {
        public Guid SubjectId { get; set; }
        public Subject Subject { get; set; } = null!;
        public Guid ClassId { get; set; }
        public Class Class { get; set; } = null!;
       
    }
}
