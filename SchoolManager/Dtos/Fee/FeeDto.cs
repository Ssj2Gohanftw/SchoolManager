using SchoolManager.Models;

namespace SchoolManager.Dtos.Fee
{
    public class FeeDto
    {
        public Guid FeeId { get; set; }
        public required int Year { get; set; }
        public required FeeType FeeType { get; set; }
        public required double Amount { get; set; } = 0.0;

    }
}
