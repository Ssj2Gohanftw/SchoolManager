using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SchoolManager.Data.Repositories.Interfaces;
using SchoolManager.Dtos.Common;
using SchoolManager.Dtos.Student;
using SchoolManager.Mappers.Students;
using SchoolManager.Models.Entities;

namespace SchoolManager.Data.Repositories
{
    public class StudentRepository : Repository<Student>, IStudentRepository
    {

        public StudentRepository(ApplicationDbContext dbContext):base(dbContext)
        {
            
        }

        public override async Task<List<Student>> GetAllAsync()
        {
            return await _entity
                .Include(s => s.Class)
                .Include(s => s.StudentSubjects)
                    .ThenInclude(ss => ss.Subject)
                .ToListAsync();
        }

        public override async Task<Student?> GetByIdAsync(Guid id)
        {
            return await _entity
                .Include(s => s.Class)
                .Include(s => s.StudentSubjects)
                    .ThenInclude(ss => ss.Subject)
                .FirstOrDefaultAsync(s => s.StudentId == id);
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

            IQueryable<Student> query = _entity
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
