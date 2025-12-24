using Microsoft.EntityFrameworkCore;
using SchoolManager.Data.Repositories.Interfaces;
using SchoolManager.Models.Entities;

namespace SchoolManager.Data.Repositories
{
    public class StudentSubjectRepository : IStudentSubjectRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly DbSet<StudentSubject> _studentSubjects;

        public StudentSubjectRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _studentSubjects = _dbContext.StudentSubjects;
        }

        public async Task AddAsync(StudentSubject studentSubject)
        {
            if (!await ExistsAsync(studentSubject.StudentId, studentSubject.SubjectId))
            {
                await _studentSubjects.AddAsync(studentSubject);
                await _dbContext.SaveChangesAsync();
            }
        }

        public Task<bool> ExistsAsync(Guid studentId, Guid subjectId)
        {
            return _studentSubjects.AnyAsync(ss => ss.StudentId == studentId && ss.SubjectId == subjectId);
        }

        public async Task<List<Subject>> GetSubjectsForStudentAsync(Guid studentId)
        {
            return await _studentSubjects
                .Where(ss => ss.StudentId == studentId)
                .Include(ss => ss.Subject)
                .Select(ss => ss.Subject)
                .ToListAsync();
        }

        public async Task<bool> RemoveAsync(Guid studentId, Guid subjectId)
        {
            var entity = await _studentSubjects.FindAsync(studentId, subjectId);
            if (entity is null) return false;
            _studentSubjects.Remove(entity);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
