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
        public async Task AddAsync(Teacher teacher)
        {
            await _teachers.AddAsync(teacher);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Teacher>> GetAllAsync()
        {
            return await _teachers.ToListAsync();
        }

        public async Task<Teacher?> GetByIdAsync(Guid id)
        {
            return await _teachers.FindAsync(id);
        }

        public async Task<bool> Remove(Teacher teacher)
        {
            _teachers.Remove(teacher);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Update(Teacher teacher)
        {
            _teachers.Update(teacher);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
