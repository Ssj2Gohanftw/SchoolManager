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

        public async Task AddAsync(Student student, CancellationToken cancellationToken = default)
        {
            await _students.AddAsync(student, cancellationToken);
        }

        public async Task<List<Student>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _students.ToListAsync(cancellationToken);
        }

        public async Task<Student?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _students.FindAsync(id, cancellationToken);
        }

        public void Remove(Student student)
        {
            _students.Remove(student);
        }
        public void Update(Student student)
        {
            _students.Update(student);
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.SaveChangesAsync(cancellationToken);
        }

        
    }
}
