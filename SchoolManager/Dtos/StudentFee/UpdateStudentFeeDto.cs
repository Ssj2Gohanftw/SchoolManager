namespace SchoolManager.Dtos.StudentFee
{
    public class UpdateStudentFeeDto
    {
        public Guid FeeId { get; set; }
        public double PaymentAmount { get; set; } = 0.0;
        public double Balance { get; set; } = 0.0;

    }
}
