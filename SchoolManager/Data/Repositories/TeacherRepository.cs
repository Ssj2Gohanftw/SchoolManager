using Microsoft.EntityFrameworkCore;
using SchoolManager.Data.Repositories.Interfaces;
using SchoolManager.Models.Entities;

namespace SchoolManager.Data.Repositories
{
    public class TeacherRepository : ITeacherRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly DbSet<Teacher> _teachers;

        public TeacherRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _teachers = dbContext.Teachers;
        }
        public async Task AddAsync(Teacher teacher, CancellationToken cancellationToken = default)
        {
            await _teachers.AddAsync(teacher, cancellationToken);
        }

        public async Task<List<Teacher>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _teachers.ToListAsync(cancellationToken);
        }

        public async Task<Teacher?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _teachers.FindAsync(id, cancellationToken);
        }

        public void Remove(Teacher teacher)
        {
             _teachers.Remove(teacher);
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public void Update(Teacher teacher)
        {
            _teachers.Update(teacher);
        }
    }
}
