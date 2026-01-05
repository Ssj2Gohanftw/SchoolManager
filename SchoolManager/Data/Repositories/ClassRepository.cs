using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SchoolManager.Data.Repositories.Interfaces;
using SchoolManager.Dtos.Class;
using SchoolManager.Dtos.Common;
using SchoolManager.Mappers.Classes;
using SchoolManager.Models.Entities;

namespace SchoolManager.Data.Repositories
{
    public class ClassRepository : Repository<Class>,IClassRepository
    {

        public ClassRepository(ApplicationDbContext dbContext):base(dbContext)
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

            return (sortBy, desc) switch
            {
                (ClassSortBy.ClassId, false) => query.OrderBy(c => c.ClassId),
                (ClassSortBy.ClassId, true) => query.OrderByDescending(c=> c.ClassId),

                (ClassSortBy.Name, true) => query.OrderByDescending(c=> c.Name),
                _ => query.OrderBy(c => c.Name),
            };
        }
        public async Task<PagedResults<Class>> GetPagedResultsAsync(ClassQueryDto classQueryDto)
        {
            classQueryDto = classQueryDto.Normalize();
            IQueryable<Class> query = _entity.AsNoTracking();
            query = classQueryDto.FilterBy switch
            {
                ClassFilterBy.Search when !string.IsNullOrWhiteSpace(classQueryDto.Search) =>
                    query.Where(c => c.Name.Contains(classQueryDto.Search!)),
                _ => query
            };
            var ordered = ApplySorting(query, classQueryDto.SortBy, classQueryDto.SortOrder)
                .ThenBy(c=>c.ClassId);

            var totalCount = await ordered.CountAsync();
            var results = await ordered
                .Skip((classQueryDto.PageNumber - 1) * classQueryDto.PageSize)
                .Take(classQueryDto.PageSize)
                .ToListAsync();

            return new PagedResults<Class>
            {
                Results = results,
                PageNumber = classQueryDto.PageNumber,
                PageSize = classQueryDto.PageSize,
                TotalCount = totalCount
            };

        }

    }
}
