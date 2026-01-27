using SchoolManager.Models;

namespace SchoolManager.Data.Repositories.Interfaces
{
    public interface IStudentFeeRepository:IRepository<StudentFee>
    {
        Task<StudentFee?> GetStudentFeeIdAsync(Guid studentId, Guid feeId);
        Task<List<Fee>> GetFeesByStudentId(Guid studentId);
    }
}
