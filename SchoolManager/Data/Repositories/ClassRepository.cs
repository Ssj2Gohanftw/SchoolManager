using Microsoft.EntityFrameworkCore;
using SchoolManager.Data.Repositories.Interfaces;
using SchoolManager.Models.Entities;

namespace SchoolManager.Data.Repositories
{
    public class ClassRepository : IClassRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly DbSet<Class> _classes;
        public ClassRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _classes = _dbContext.Class;
        }
        public async Task AddAsync(Class @class)
        {
             await _classes.AddAsync(@class);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Class>> GetAllAsync()
        {
            return await _classes
                //.Include(c => c.Students)
                .ToListAsync();
        }

        public async Task<Class?> GetByIdAsync(Guid id)
        {
            return await _classes
                .Include(c => c.Students)
                    .ThenInclude(s => s.StudentSubjects)
                        .ThenInclude(ss => ss.Subject)
                .FirstOrDefaultAsync(c => c.ClassId == id);
        }

        public async Task<Class?> GetByNameAsync(string name)
        {
            var normalized = name.Trim();
            return await _classes.FirstOrDefaultAsync(c => c.Name == normalized);
        }

        public async Task<bool> Remove(Class @class)
        {
            _classes.Remove(@class);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Update(Class @class)
        {
            _classes.Update(@class);
            await _dbContext.SaveChangesAsync();
            return true;

        }
    }
}
