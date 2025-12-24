using Microsoft.EntityFrameworkCore;
using SchoolManager.Data.Repositories.Interfaces;
using SchoolManager.Models.Entities;

namespace SchoolManager.Data.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly DbSet<Student> _students;

        public StudentRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _students = _dbContext.Students;
        }

        public async Task AddAsync(Student student)
        {
            await _students.AddAsync(student);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Student>> GetAllAsync()
        {
            return await _students
                .Include(s => s.Class)
                .Include(s => s.StudentSubjects)
                    .ThenInclude(ss => ss.Subject)
                .ToListAsync();
        }
        public async Task<List<Student>> GetAllSortedAsync()
        {
            return await _students.OrderByDescending(s => s.FirstName)
                .Include(s => s.Class)
                .Include(s => s.StudentSubjects)
                    .ThenInclude(ss => ss.Subject)
                .ToListAsync();
        }

        public async Task<Student?> GetByIdAsync(Guid id)
        {
            return await _students
                .Include(s => s.Class)
                .Include(s => s.StudentSubjects)
                    .ThenInclude(ss => ss.Subject)
                .FirstOrDefaultAsync(s => s.StudentId == id);
        }

        public async Task<bool> Remove(Student student)
        {
            _students.Remove(student);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Update(Student student)
        {
             _students.Update(student);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        
    }
}
