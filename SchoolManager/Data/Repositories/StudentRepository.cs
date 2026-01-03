using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SchoolManager.Data.Repositories.Interfaces;
using SchoolManager.Dtos.Common;
using SchoolManager.Dtos.Student;
using SchoolManager.Mappers.Students;
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

        private static IOrderedQueryable<Student> ApplySorting(IQueryable<Student> query, StudentSortBy sortBy, SortOrder SortOrder)
        {
            var desc = SortOrder == SortOrder.Descending;

            return (sortBy, desc) switch
            {
                (StudentSortBy.LastName, false) => query.OrderBy(s => s.LastName),
                (StudentSortBy.LastName, true) => query.OrderByDescending(s => s.LastName),

                (StudentSortBy.Email, false) => query.OrderBy(s => s.Email),
                (StudentSortBy.Email, true) => query.OrderByDescending(s => s.Email),

                (StudentSortBy.DateOfBirth, false) => query.OrderBy(s => s.DateOfBirth),
                (StudentSortBy.DateOfBirth, true) => query.OrderByDescending(s => s.DateOfBirth),

                (StudentSortBy.ClassName, false) => query.OrderBy(s => s.Class!.Name),
                (StudentSortBy.ClassName, true) => query.OrderByDescending(s => s.Class!.Name),

                (StudentSortBy.FirstName, true) => query.OrderByDescending(s => s.FirstName),
                _ => query.OrderBy(s => s.FirstName)
            };
        }

        public async Task<PagedResults<Student>> GetPagedAsync(StudentQueryDto studentQueryDto)
        {
            studentQueryDto = studentQueryDto.Normalize();

            IQueryable<Student> query = _students
                .AsNoTracking()
                .Include(s => s.Class)
                .Include(s => s.StudentSubjects)
                    .ThenInclude(s => s.Subject);

            query = studentQueryDto.FilterBy switch
            {
                StudentFilterBy.ClassId when studentQueryDto.ClassId is not null => query.Where(s => s.ClassId == studentQueryDto.ClassId),

                StudentFilterBy.Search when !string.IsNullOrWhiteSpace(studentQueryDto.Search) =>
                    query.Where(s =>
                        s.FirstName.Contains(studentQueryDto.Search!) ||
                        s.LastName.Contains(studentQueryDto.Search!) ||
                        s.Email.Contains(studentQueryDto.Search!)),

                _ => query
            };

            // important: stable ordering for pagination
            var ordered= ApplySorting(query, studentQueryDto.SortBy, studentQueryDto.SortOrder)
                .ThenBy(s => s.StudentId);

            var totalCount = await ordered.CountAsync();

            var items = await ordered
                .Skip((studentQueryDto.PageNumber - 1) * studentQueryDto.PageSize)
                .Take(studentQueryDto.PageSize)
                .ToListAsync();

            return new PagedResults<Student>
            {
                Results = items,
                PageNumber = studentQueryDto.PageNumber,
                PageSize = studentQueryDto.PageSize,
                TotalCount = totalCount
            };
        }
    }
}
