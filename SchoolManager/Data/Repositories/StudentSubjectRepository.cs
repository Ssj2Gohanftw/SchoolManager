using Microsoft.EntityFrameworkCore;
using SchoolManager.Data.Repositories.Interfaces;
using SchoolManager.Models;

namespace SchoolManager.Data.Repositories
{
    public class StudentSubjectRepository :Repository<StudentSubject>, IStudentSubjectRepository
    {
       
        public StudentSubjectRepository(ApplicationDbContext dbContext):base(dbContext)
        {
            
        }

        public async Task AddAsync(StudentSubject studentSubject)
        {
            if (!await ExistsAsync(studentSubject.StudentId, studentSubject.SubjectId))
            {
                await _entity.AddAsync(studentSubject);
                await _dbContext.SaveChangesAsync();
            }
        }

        public Task<bool> ExistsAsync(Guid studentId, Guid subjectId)
        {
            return _entity.AnyAsync(ss => ss.StudentId == studentId && ss.SubjectId == subjectId);
        }

        public async Task<List<Subject>> GetSubjectsForStudentAsync(Guid studentId)
        {
            return await _entity
                .Where(ss => ss.StudentId == studentId)
                .Include(ss => ss.Subject)
                .Select(ss => ss.Subject)
                .ToListAsync();
        }

        public async Task<bool> RemoveAsync(Guid studentId, Guid subjectId)
        {
            StudentSubject? entity = await _entity.FindAsync(studentId, subjectId);
            if (entity == null) return false;
            _entity.Remove(entity);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
