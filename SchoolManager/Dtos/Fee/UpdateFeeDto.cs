using SchoolManager.Models;

namespace SchoolManager.Dtos.Fee
{
    public class UpdateFeeDto
    {
        public int Year { get; set; }
        public FeeType FeeType { get; set; }
        public double Amount { get; set; }
    }
}
