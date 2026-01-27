using Microsoft.EntityFrameworkCore;
using SchoolManager.Data.Repositories.Interfaces;
using SchoolManager.Models;

namespace SchoolManager.Data.Repositories
{
    public class FeeRepository:Repository<Fee>,IFeeRepository
    {
        public FeeRepository(ApplicationDbContext dbContext):base(dbContext)
        {
            
        }
        public override async Task<Fee?> GetByIdAsync(Guid id)
        {
            return await _entity
                .Include(s => s.StudentFees)
                .FirstOrDefaultAsync(f => f.FeeId == id);
        }
        public override async Task<List<Fee>> GetAllAsync()
        {
            return await _entity
                .Include(s => s.StudentFees)
                .ToListAsync();
        }

    }
}
