using SchoolManager.Dtos.Fee;
using SchoolManager.Models;

namespace SchoolManager.Mappers.Fees
{
    public static class FeeMapper
    {
        public static FeeDto ToFeesDto(this Fee fee)
        {
            return new FeeDto
            {
                FeeId = fee.FeeId,
                FeeType = fee.FeeType,
                Year =fee.Year,
                Amount = fee.Amount
            };
        }
        public static Fee ToFees(this AddFeeDto addFeeDto)
        {
            var feeYear = addFeeDto.Year;
            if (feeYear <= 0)
            {
                feeYear = DateTime.Now.Year;
            }
            return new Fee
            {
                FeeType = addFeeDto.FeeType,
                Year = feeYear,
                Amount = addFeeDto.Amount,

            };
        }
        public static void ToUpdateFeesDto(this Fee fee,UpdateFeeDto updateFeeDto)
        {
            if (updateFeeDto.FeeType != null) fee.FeeType = updateFeeDto.FeeType;
            if (updateFeeDto.Amount != null) fee.Amount = updateFeeDto.Amount;
            if (updateFeeDto.Year!= null) fee.Year= updateFeeDto.Year;
        }
        
    }

}
