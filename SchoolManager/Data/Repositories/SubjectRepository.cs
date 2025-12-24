using Microsoft.EntityFrameworkCore;
using SchoolManager.Data.Repositories.Interfaces;
using SchoolManager.Models.Entities;

namespace SchoolManager.Data.Repositories
{
    public class SubjectRepository : ISubjectRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly DbSet<Subject> _subject;

        public SubjectRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _subject = _dbContext.Subjects;
        }
        public async Task AddAsync(Subject subject)
        {
            await _subject.AddAsync(subject);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Subject>> GetAllAsync()
        {
            return await _subject.ToListAsync();
            
        }

        public async Task<Subject?> GetByIdAsync(Guid id)
        {
            return await _subject.FindAsync(id);
        }

        public async Task<bool> Remove(Subject subject)
        {
            _subject.Remove(subject);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Update(Subject subject)
        {
            _subject.Update(subject);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
