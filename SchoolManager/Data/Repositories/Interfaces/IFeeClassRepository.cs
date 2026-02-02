using SchoolManager.Models;

namespace SchoolManager.Data.Repositories.Interfaces
{
    public interface IFeeClassRepository:IRepository<FeeClass>
    {
        Task<List<Fee>> GetAllFeeDetailsFromClass(Guid classId);

        //Task<Class> GetBranchAsync(Branch branch);
        Task<bool> ExistsAsync(Guid classId, List<Guid> feeIds);
        Task AssignFeestoClassAsync(List<Guid> feeIds, Guid classId);
        
    }
}
