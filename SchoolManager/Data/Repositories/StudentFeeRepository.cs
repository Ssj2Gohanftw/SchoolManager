using Microsoft.EntityFrameworkCore;
using SchoolManager.Data.Repositories.Interfaces;
using SchoolManager.Models;

namespace SchoolManager.Data.Repositories
{
    public class StudentFeeRepository : Repository<StudentFee>, IStudentFeeRepository
    {
        public StudentFeeRepository(ApplicationDbContext dbContext):base(dbContext)
        {
            
        }
        public async Task<List<Fee>> GetFeesByStudentId(Guid studentId)
        {
            return await _entity
                .Include(f=>f.Fee)
                .Include(s => s.Student)
                .Where(sf => sf.StudentId == studentId)
                .Select(s => s.Fee).ToListAsync();
        }

        public async Task<StudentFee?> GetStudentFeeIdAsync(Guid studentId, Guid feeId)
        {
            return await _entity
                .Include(sf=>sf.Fee)
                .Include(sf=>sf.Student)
                .FirstOrDefaultAsync(sf=>sf.StudentId == studentId
                                         && sf.FeeId == feeId);
        }

        
    }
}
