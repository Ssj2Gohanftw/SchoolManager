using SchoolManager.Dtos;
using SchoolManager.Dtos.Fee;
using SchoolManager.Models;

namespace SchoolManager.Mappers
{
    public static class FeeClassMapper
    {
        public static FeeClassDto ToFeeClassDto(this FeeClass feeClass)
        {
            return new FeeClassDto
            {
                FeeId = feeClass.FeeId,
                ClassId = feeClass.ClassId,
                FeeName = feeClass.Fee.FeeType,
                ClassName=feeClass.Class.Name
            };
        }
        public static FeeDto ToFeeDto(this FeeClass feeClass)
        {
            return new FeeDto
            {
                FeeId = feeClass.FeeId,
                Amount=feeClass.Fee.Amount,
                FeeType=feeClass.Fee.FeeType,
                Year=feeClass.Fee.Year
            };
        }

    }
}
