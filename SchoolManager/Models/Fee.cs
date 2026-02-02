namespace SchoolManager.Models
{
    public enum FeeType
    {
        Tuition,
        Library,
        Laboratory,
        Sports,
        Examination,
        Miscellaneous
    }
    public class Fee
    {
        public Guid FeeId { get; set; }
        public required int Year { get; set; }
        public required FeeType FeeType { get; set; }
        public required double Amount { get; set; } = 0.0;
        public List<StudentFee> StudentFees { get; set; } = new List<StudentFee>();
        public List<Class> Classes { get; set; } = new List<Class>();
        public List<FeeClass> FeeClasses { get; set; }
            = new List<FeeClass>();
    }
}
