using SchoolManager.Dtos.StudentFee;
using SchoolManager.Models;

namespace SchoolManager.Mappers
{
    public static class StudentFeeMapper
    {
        public static void ToUpdateStudentFeeDto(this StudentFee studentFee, UpdateStudentFeeDto updateStudentFeeDto)
        {
            if (updateStudentFeeDto.FeeId != null) studentFee.FeeId = updateStudentFeeDto.FeeId;
            studentFee.AmountPaid += updateStudentFeeDto.PaymentAmount;
            studentFee.Balance = studentFee.Fee.Amount - studentFee.AmountPaid;           
        }
    }
}
