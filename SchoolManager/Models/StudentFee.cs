namespace SchoolManager.Models
{
    public class StudentFee
    {
        public Guid StudentId { get; set; }
        public Student Student { get; set; }
        public Guid FeeId { get; set; }
        public Fee Fee { get; set; }
        public double AmountPaid { get; set; } = 0.0;
        public double Balance { get; set; } = 0.0;
    }
}
