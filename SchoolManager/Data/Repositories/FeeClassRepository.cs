using Microsoft.EntityFrameworkCore;
using SchoolManager.Data.Repositories.Interfaces;
using SchoolManager.Models;

namespace SchoolManager.Data.Repositories
{
    public class FeeClassRepository : Repository<FeeClass>, IFeeClassRepository
    {
        public FeeClassRepository(ApplicationDbContext dbContext):base(dbContext)
        {
            
        }

        public async Task AssignFeestoClassAsync(List<Guid> feeIds, Guid classId)
        {
            List<Guid> existingFees = await _entity
                .Where(fc=>fc.ClassId==classId)
                .Select(fc => fc.FeeId).ToListAsync();

            List<FeeClass> feesToAssign = feeIds
                .Except(existingFees)
                .Select(feeIds => new FeeClass
                {
                    ClassId = classId,
                    FeeId = feeIds
                }).ToList();
            if (feesToAssign.Any())
            {
                await _entity.AddRangeAsync(feesToAssign);
            }
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(Guid classId, List<Guid> feeIds)
        {
            return await _entity.AnyAsync(fc => fc.ClassId == classId && feeIds.Contains(fc.FeeId));
        }

        public async Task<List<Fee>> GetAllFeeDetailsFromClass(Guid classId)
        {
             var classFees= await _entity
                .Include(f => f.Fee)
                .Include(f => f.Class)
                .Where(fc => fc.ClassId == classId)
                .Select(f=>f.Fee)
                .ToListAsync();
            return classFees;
        }

    }
}
