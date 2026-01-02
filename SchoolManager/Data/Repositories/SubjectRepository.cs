using Microsoft.EntityFrameworkCore;
using SchoolManager.Data.Repositories.Interfaces;
using SchoolManager.Dtos.Common;
using SchoolManager.Dtos.Subject;
using SchoolManager.Mappers.Subjects;
using SchoolManager.Models.Entities;

namespace SchoolManager.Data.Repositories
{
    public class SubjectRepository : ISubjectRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly DbSet<Subject> _subject;

        public SubjectRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _subject = _dbContext.Subjects;
        }
        public async Task AddAsync(Subject subject)
        {
            await _subject.AddAsync(subject);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Subject>> GetAllAsync()
        {
            return await _subject.ToListAsync();
            
        }

        public async Task<Subject?> GetByIdAsync(Guid id)
        {
            return await _subject.FindAsync(id);
        }
        private static IOrderedQueryable<Subject> ApplySorting(IQueryable<Subject> query, SubjectSortBy sortBy, SortDirection sortDirection)
        {
            var desc = sortDirection == SortDirection.Desc;

            return (sortBy, desc) switch
            {
                (SubjectSortBy.Name, false) => query.OrderBy(sub => sub.Name),
                (SubjectSortBy.Name, true) => query.OrderByDescending(sub => sub.Name),

                (SubjectSortBy.SubjectId, false) => query.OrderBy(sub => sub.SubjectId),
                (SubjectSortBy.SubjectId, true) => query.OrderByDescending(sub => sub.SubjectId),

                _ => query.OrderBy(sub=> sub.Name)
            };
        }

        public async Task<PagedResults<Subject>> GetPagedResults(SubjectQueryDto subjectQueryDto)
        {
            subjectQueryDto = subjectQueryDto.Normalize();

            IQueryable<Subject> query = _subject.AsNoTracking();

            query = subjectQueryDto.FilterBy switch
            {
                SubjectFilterBy.Search when !string.IsNullOrWhiteSpace(subjectQueryDto.Search) =>
                   query.Where(c => c.Name.Contains(subjectQueryDto.Search!)),
                _ => query
            };

            var ordered = ApplySorting(query, subjectQueryDto.SortBy, subjectQueryDto.SortDirection)
                .ThenBy(sub => sub.SubjectId);

            var totalCount = await ordered.CountAsync();

            var items = await ordered
                .Skip((subjectQueryDto.PageNumber - 1) * subjectQueryDto.PageSize)
                .Take(subjectQueryDto.PageSize)
                .ToListAsync();

            return new PagedResults<Subject>
            {
                Items = items,
                PageNumber = subjectQueryDto.PageNumber,
                PageSize = subjectQueryDto.PageSize,
                TotalCount = totalCount
            };
        }

        public async Task<bool> Remove(Subject subject)
        {
            _subject.Remove(subject);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Update(Subject subject)
        {
            _subject.Update(subject);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
