using SchoolManager.Models;

namespace SchoolManager.Dtos.Fee
{
    public class AddFeeDto
    {
        public int Year { get; set; } 
        public required FeeType FeeType { get; set; } = FeeType.Tuition;
        public required double Amount { get; set; } = 0.0;

    }
}
