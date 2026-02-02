using SchoolManager.Dtos.StudentFee;
using SchoolManager.Models;

namespace SchoolManager.Services.Interfaces
{
    public interface IStudentFeeServices
    {
        Task<List<Fee>> AssignFeesToStudents(List<Guid> feeIds);
        Task<List<Fee>> GetFeesByStudentIdAsync(Guid guid);
        Task<bool> PayFeesAsync(Guid studentId, UpdateStudentFeeDto updateStudentFeeDto);
        //Task<bool> ClearFeesAsync(Guid studentId,Guid feeId); 
    }
}
