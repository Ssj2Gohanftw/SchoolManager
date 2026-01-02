using Microsoft.EntityFrameworkCore;
using SchoolManager.Data.Repositories.Interfaces;
using SchoolManager.Dtos.Class;
using SchoolManager.Dtos.Common;
using SchoolManager.Mappers.Classes;
using SchoolManager.Models.Dtos.Student;
using SchoolManager.Models.Entities;

namespace SchoolManager.Data.Repositories
{
    public class ClassRepository : IClassRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly DbSet<Class> _classes;
        public ClassRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _classes = _dbContext.Class;
        }
        public async Task AddAsync(Class @class)
        {
             await _classes.AddAsync(@class);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Class>> GetAllAsync()
        {
            return await _classes
                //.Include(c => c.Students)
                .ToListAsync();
        }

        public async Task<Class?> GetByIdAsync(Guid id)
        {
            return await _classes
                .Include(c => c.Students)
                    .ThenInclude(s => s.StudentSubjects)
                        .ThenInclude(ss => ss.Subject)
                .FirstOrDefaultAsync(c => c.ClassId == id);
        }

        public async Task<Class?> GetByNameAsync(string name)
        {
            var normalized = name.Trim();
            return await _classes.FirstOrDefaultAsync(c => c.Name == normalized);
        }

        private static IOrderedQueryable<Class> ApplySorting(
            IQueryable<Class> query, 
            ClassSortBy sortBy, 
            SortDirection sortDirection)
        {
            var desc = sortDirection == SortDirection.Desc;

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
            IQueryable<Class> query = _classes.AsNoTracking();
            query = classQueryDto.FilterBy switch
            {
                ClassFilterBy.Search when !string.IsNullOrWhiteSpace(classQueryDto.Search) =>
                    query.Where(c => c.Name.Contains(classQueryDto.Search!)),
                _ => query
            };
            var ordered = ApplySorting(query, classQueryDto.SortBy, classQueryDto.SortDirection)
                .ThenBy(c=>c.ClassId);

            var totalCount = await ordered.CountAsync();
            var items = await ordered
                .Skip((classQueryDto.PageNumber - 1) * classQueryDto.PageSize)
                .Take(classQueryDto.PageSize)
                .ToListAsync();

            return new PagedResults<Class>
            {
                Items = items,
                PageNumber = classQueryDto.PageNumber,
                PageSize = classQueryDto.PageSize,
                TotalCount = totalCount
            };
        }

        public async Task<bool> Remove(Class @class)
        {
            _classes.Remove(@class);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Update(Class @class)
        {
            _classes.Update(@class);
            await _dbContext.SaveChangesAsync();
            return true;

        }
    }
}
