using LinqKit;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SchoolManager.Data.Repositories.Interfaces;
using SchoolManager.Dtos.Class;
using SchoolManager.Dtos.Common;
using SchoolManager.Mappers.Classes;
using SchoolManager.Mappers.Common;
using SchoolManager.Models.Entities;
using System.Linq.Expressions;

namespace SchoolManager.Data.Repositories
{
    public class ClassRepository : Repository<Class>, IClassRepository
    {

        public ClassRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }

        public override async Task<Class?> GetByIdAsync(Guid id)
        {
            return await _entity
                .Include(c => c.Students)
                    .ThenInclude(s => s.StudentSubjects)
                        .ThenInclude(ss => ss.Subject)
                .FirstOrDefaultAsync(c => c.ClassId == id);
        }

        public async Task<Class?> GetByNameAsync(string name)
        {
            var normalized = name.Trim();
            return await _entity.FirstOrDefaultAsync(c => c.Name == normalized);
        }

        private static IOrderedQueryable<Class> ApplySorting(
            IQueryable<Class> query,
            ClassSortBy sortBy,
            SortOrder SortOrder)
        {
            var desc = SortOrder == SortOrder.Descending;
            IOrderedQueryable<Class> orderedQuery;
            switch (sortBy, desc)
            {
                case (ClassSortBy.ClassId, false):
                    orderedQuery = query.OrderBy(c => c.ClassId);
                    break;
                case (ClassSortBy.ClassId, true):
                    orderedQuery = query.OrderByDescending(c => c.ClassId);
                    break;
                case (ClassSortBy.Name, false):
                    orderedQuery = query.OrderBy(c => c.Name);
                    break;
                default:
                    orderedQuery = query.OrderByDescending(c => c.Name);
                    break;

            }
            return orderedQuery;
        }
        public async Task<PagedResults<Class>> GetPagedResultsAsync(ClassQueryDto classQueryDto)
        {
            classQueryDto = classQueryDto.Normalize();
            var searchFilter = SearchFilter(classQueryDto.Search);
            IQueryable<Class> query = _entity
                .AsNoTracking()
                .AsExpandableEFCore()
                .Where(searchFilter);

            var ordered = ApplySorting(query, classQueryDto.SortBy, classQueryDto.SortOrder)
                .ThenBy(c => c.ClassId);

            var totalCount = await ordered.CountAsync();
            var results = await ordered
                .Skip((classQueryDto.PageNumber - 1) * classQueryDto.PageSize)
                .Take(classQueryDto.PageSize)
                .ToListAsync();

            return results.ToPagedResults(classQueryDto.PageNumber, classQueryDto.PageSize, totalCount);

        }
        public static Expression<Func<Class, bool>> SearchFilter(string? search)
        {
            var query = PredicateBuilder.New<Class>(false);
            if (string.IsNullOrWhiteSpace(search))
            {
                return PredicateBuilder.New<Class>(true);

            }
            query = query.Or(c => c.Name.Contains(search));
            return query;
        }

    }
}
