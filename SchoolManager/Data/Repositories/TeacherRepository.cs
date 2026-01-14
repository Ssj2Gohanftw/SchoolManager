using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SchoolManager.Data.Repositories.Interfaces;
using SchoolManager.Dtos.Common;
using SchoolManager.Dtos.Teacher;
using SchoolManager.Mappers.Common;
using SchoolManager.Mappers.Teachers;
using SchoolManager.Models.Entities;


namespace SchoolManager.Data.Repositories
{
    public class TeacherRepository : Repository<Teacher>, ITeacherRepository
    {
        public TeacherRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }
        public override async Task<Teacher?> GetByIdAsync(Guid id)
        {
            return await _entity
                .Include(t => t.SubjectTeachers)
                    .ThenInclude(st => st.Class)
                .Include(t => t.SubjectTeachers)
                    .ThenInclude(st => st.Subject)
                .FirstOrDefaultAsync(t => t.TeacherId == id);
        }
        private static IOrderedQueryable<Teacher> ApplySorting(IQueryable<Teacher> query, TeacherSortBy sortBy, SortOrder SortOrder)
        {
            var desc = SortOrder == SortOrder.Descending;
            IOrderedQueryable<Teacher> orderedQuery;
            switch (sortBy, desc)
            {
                case (TeacherSortBy.Name, false):
                    orderedQuery = query.OrderBy(t => t.FirstName).ThenBy(t => t.LastName);
                    break;
                case (TeacherSortBy.Email, false):
                    orderedQuery = query.OrderBy(t => t.Email);
                    break;
                case (TeacherSortBy.Email, true):
                    orderedQuery = query.OrderByDescending(t => t.Email);
                    break;
                default:
                    orderedQuery = query.OrderBy(t => t.FirstName).ThenBy(t => t.LastName);
                    break;
            }
            ;
            return orderedQuery;
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

            IQueryable<Teacher> query = _entity
                .AsNoTracking()
                .Include(t => t.SubjectTeachers)
                    .ThenInclude(st => st.Class)
                .Include(t => t.SubjectTeachers)
                    .ThenInclude(st => st.Subject);

            query = teacherQueryDto.FilterBy switch
            {
                FilterBy.Search when !string.IsNullOrWhiteSpace(teacherQueryDto.Search) =>
                    ApplyTeacherSearch(query, teacherQueryDto.Search!),
                _ => query
            };

            var ordered = ApplySorting(query, teacherQueryDto.SortBy, teacherQueryDto.SortOrder)
                .ThenBy(t => t.TeacherId);

            var totalCount = await ordered.CountAsync();

            var results = await ordered
                .Skip((teacherQueryDto.PageNumber - 1) * teacherQueryDto.PageSize)
                .Take(teacherQueryDto.PageSize)
                .ToListAsync();

            return results.ToPagedResults(teacherQueryDto.PageNumber, teacherQueryDto.PageSize, totalCount);
            //return new PagedResults<Teacher>
            //{
            //    Results = results,
            //    PageNumber = teacherQueryDto.PageNumber,
            //    PageSize = teacherQueryDto.PageSize,
            //    TotalCount = totalCount
            //};
        }
    }
}
