using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SchoolManager.Data.Repositories.Interfaces;
using SchoolManager.Dtos.Common;
using SchoolManager.Dtos.Subject;
using SchoolManager.Mappers.Subjects;
using SchoolManager.Models.Entities;

namespace SchoolManager.Data.Repositories
{
    public class SubjectRepository : Repository<Subject>,ISubjectRepository
    {
        
        public SubjectRepository(ApplicationDbContext dbContext):base(dbContext)
        {

        }
        private static IOrderedQueryable<Subject> ApplySorting(IQueryable<Subject> query, SubjectSortBy sortBy, SortOrder SortOrder)
        {
            var desc = SortOrder == SortOrder.Descending;

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

            IQueryable<Subject> query = _entity.AsNoTracking();

            query = subjectQueryDto.FilterBy switch
            {
                SubjectFilterBy.Search when !string.IsNullOrWhiteSpace(subjectQueryDto.Search) =>
                   query.Where(c => c.Name.Contains(subjectQueryDto.Search!)),
                _ => query
            };

            var ordered = ApplySorting(query, subjectQueryDto.SortBy, subjectQueryDto.SortOrder)
                .ThenBy(sub => sub.SubjectId);

            var totalCount = await ordered.CountAsync();

            var items = await ordered
                .Skip((subjectQueryDto.PageNumber - 1) * subjectQueryDto.PageSize)
                .Take(subjectQueryDto.PageSize)
                .ToListAsync();

            return new PagedResults<Subject>
            {
                Results = items,
                PageNumber = subjectQueryDto.PageNumber,
                PageSize = subjectQueryDto.PageSize,
                TotalCount = totalCount
            };
        }
    }
}
