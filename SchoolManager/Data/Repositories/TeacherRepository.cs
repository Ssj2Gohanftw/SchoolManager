using Microsoft.EntityFrameworkCore;
using SchoolManager.Data.Repositories.Interfaces;
using SchoolManager.Dtos.Common;
using SchoolManager.Dtos.Teacher;
using SchoolManager.Mappers.Teachers;
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
            => await _teachers.ToListAsync();

        public async Task<Teacher?> GetByIdAsync(Guid id)
            => await _teachers.FindAsync(id);

        public async Task<Teacher?> GetByIdWithAssignmentsAsync(Guid id)
        {
            return await _teachers
                .Include(t => t.SubjectTeachers)
                    .ThenInclude(st => st.Class)
                .Include(t => t.SubjectTeachers)
                    .ThenInclude(st => st.Subject)
                .FirstOrDefaultAsync(t => t.TeacherId == id);
        }
        private static IOrderedQueryable<Teacher> ApplySorting(IQueryable<Teacher> query, TeacherSortBy sortBy, SortDirection sortDirection)
        {
            var desc = sortDirection == SortDirection.Desc;

            return (sortBy, desc) switch
            {
                (TeacherSortBy.Name, false) => query.OrderBy(t => t.FirstName).ThenBy(t => t.FirstName),
                (TeacherSortBy.Name, true) => query.OrderByDescending(t => t.FirstName).ThenBy(t => t.LastName),

                (TeacherSortBy.Email, false) => query.OrderBy(t => t.Email),
                (TeacherSortBy.Email, true) => query.OrderByDescending(t => t.Email),

                _ => query.OrderBy(t => t.FirstName).ThenBy(t => t.LastName)
            };
        }
        private static IQueryable<Teacher> ApplyTeacherSearch(IQueryable<Teacher> query, string search)
        {
            search = search.Trim();

            // Tokenize "john doe" -> ["john","doe"]
            var tokens = search
                .Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

            // 1) Token approach: each token must match either first or last name (AND across tokens)
            foreach (var token in tokens)
            {
                var t = token;
                query = query.Where(x =>
                    EF.Functions.ILike(x.FirstName, $"%{t}%") ||
                    EF.Functions.ILike(x.LastName, $"%{t}%") ||
                    EF.Functions.ILike(x.Email, $"%{t}%") 
                );
            }

            //// 2) Full-string approach to support exact-ish "first last" and "last first"
            //// (keeps behavior intuitive when search contains spaces)
            //query = query.Where(x =>
            //    x.Email.Contains(search) ||
            //    (x.FirstName + " " + x.LastName).Contains(search) ||
            //    (x.LastName + " " + x.FirstName).Contains(search));

            return query;
        }
        public async Task<PagedResults<Teacher>> GetPagedAsync(TeacherQueryDto teacherQueryDto)
        {
            teacherQueryDto = teacherQueryDto.Normalize();

            IQueryable<Teacher> query = _teachers.AsNoTracking();

            query = teacherQueryDto.FilterBy switch
            {
                TeacherFilterBy.Search when !string.IsNullOrWhiteSpace(teacherQueryDto.Search) =>
                    ApplyTeacherSearch(query,teacherQueryDto.Search!),
                _ => query
            };

            var ordered= ApplySorting(query, teacherQueryDto.SortBy, teacherQueryDto.SortDirection)
                .ThenBy(t => t.TeacherId);

            var totalCount = await ordered.CountAsync();

            var items = await ordered
                .Skip((teacherQueryDto.PageNumber - 1) * teacherQueryDto.PageSize)
                .Take(teacherQueryDto.PageSize)
                .ToListAsync();

            return new PagedResults<Teacher>
            {
                Items = items,
                PageNumber = teacherQueryDto.PageNumber,
                PageSize = teacherQueryDto.PageSize,
                TotalCount = totalCount
            };
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
