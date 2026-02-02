namespace SchoolManager.Models
{
    public class FeeClass
    {
        public Guid FeeId { get; set; }
        public Fee Fee { get; set; }
        public Guid ClassId { get; set; }
        public Class Class { get; set; }
    }
}
