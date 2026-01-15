using LinqKit;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SchoolManager.Data.Repositories.Interfaces;
using SchoolManager.Dtos.Common;
using SchoolManager.Dtos.Teacher;
using SchoolManager.Mappers.Common;
using SchoolManager.Mappers.Teachers;
using SchoolManager.Models.Entities;
using System.Linq.Expressions;


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
                case (TeacherSortBy.FirstName, true):
                    orderedQuery = query.OrderByDescending(t => t.FirstName);
                    break;
                case (TeacherSortBy.LastName, false):
                    orderedQuery = query.OrderBy(t => t.LastName);
                    break;
                case (TeacherSortBy.LastName, true):
                    orderedQuery = query.OrderByDescending(t => t.LastName);
                    break;
                case (TeacherSortBy.Email, false):
                    orderedQuery = query.OrderBy(t => t.Email);
                    break;
                case (TeacherSortBy.Email, true):
                    orderedQuery = query.OrderByDescending(t => t.Email);
                    break;
                default:
                    orderedQuery = query.OrderBy(t => t.FirstName);
                    break;
            }
            
            return orderedQuery;
        }
        //private static IQueryable<Teacher> ApplyTeacherSearch(IQueryable<Teacher> query, string search)
        //{
        //    search = search.Trim();

        //    // Tokenize "john doe" -> ["john","doe"]
        //    var tokens = search
        //        .Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

        //    // 1) Token approach: each token must match either first or last name (AND across tokens)
        //    foreach (var token in tokens)
        //    {
        //        var t = token;
        //        query = query.Where(x =>
        //            EF.Functions.ILike(x.FirstName, $"%{t}%") ||
        //            EF.Functions.ILike(x.LastName, $"%{t}%") ||
        //            EF.Functions.ILike(x.Email, $"%{t}%")
        //        );
        //    }

        //    return query;
        //}
        public async Task<PagedResults<Teacher>> GetPagedAsync(TeacherQueryDto teacherQueryDto)
        {
            teacherQueryDto = teacherQueryDto.Normalize();
            var searchFilter=SearchFilter(teacherQueryDto.Search);

            IQueryable<Teacher> query = _entity
                .AsNoTracking()
                .Include(t => t.SubjectTeachers)
                    .ThenInclude(st => st.Class)
                .Include(t => t.SubjectTeachers)
                    .ThenInclude(st => st.Subject)
                .Where(searchFilter);

            var ordered = ApplySorting(query, teacherQueryDto.SortBy, teacherQueryDto.SortOrder)
                .ThenBy(t => t.TeacherId);

            var totalCount = await ordered.CountAsync();

            var results = await ordered
                .Skip((teacherQueryDto.PageNumber - 1) * teacherQueryDto.PageSize)
                .Take(teacherQueryDto.PageSize)
                .ToListAsync();

            return results.ToPagedResults(teacherQueryDto.PageNumber, teacherQueryDto.PageSize, totalCount);
            
        }
        public static Expression<Func<Teacher,bool>> SearchFilter(string? search) 
        {
            var query = PredicateBuilder.New<Teacher>(true);
            if (string.IsNullOrWhiteSpace(search)) 
            {
                return PredicateBuilder.New<Teacher>(false);
            }
            query = query.Or(t => t.FirstName.Contains(search));
            query = query.Or(t => t.LastName.Contains(search));
            query = query.Or(t => t.Email.Contains(search));
            return query;
        }
    }
}
