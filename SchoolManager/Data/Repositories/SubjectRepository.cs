using LinqKit;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SchoolManager.Data.Repositories.Interfaces;
using SchoolManager.Dtos.Common;
using SchoolManager.Dtos.Subject;
using SchoolManager.Mappers.Subjects;
using SchoolManager.Models.Entities;
using System.Linq.Expressions;

namespace SchoolManager.Data.Repositories
{
    public class SubjectRepository : Repository<Subject>,ISubjectRepository
    {
        
        public SubjectRepository(ApplicationDbContext dbContext):base(dbContext)
        {

        }

        public override async Task<Subject?> GetByIdAsync(Guid id)
        {
            return await _entity
                .AsNoTracking()
                .Include(sub => sub.SubjectTeachers)
                    .ThenInclude(st => st.Teacher)
                .Include(sub=>sub.SubjectTeachers)
                    .ThenInclude(c=>c.Class)
                .FirstOrDefaultAsync(sub => sub.SubjectId == id);
        }
        private static IOrderedQueryable<Subject> ApplySorting(IQueryable<Subject> query, SubjectSortBy sortBy, SortOrder SortOrder)
        {
            var desc = SortOrder == SortOrder.Descending;
            IOrderedQueryable<Subject> orderedQuery;
            switch(sortBy, desc) 
            {
                case (SubjectSortBy.Name, false):
                    orderedQuery=query.OrderBy(sub => sub.Name);
                    break;
                case (SubjectSortBy.Name, true):
                    orderedQuery = query.OrderByDescending(sub => sub.Name);
                    break;
                case (SubjectSortBy.SubjectId, false):
                    orderedQuery = query.OrderBy(sub => sub.SubjectId);
                    break;
                case (SubjectSortBy.SubjectId, true):
                    orderedQuery = query.OrderByDescending(sub => sub.SubjectId);
                    break;
                default:
                    orderedQuery = query.OrderBy(sub => sub.Name);
                    break;
            }
            return orderedQuery;
            
        }

        public async Task<PagedResults<Subject>> GetPagedResults(SubjectQueryDto subjectQueryDto)
        {
            subjectQueryDto = subjectQueryDto.Normalize();
            var searchFilter = SearchFilter(subjectQueryDto.Search).Expand();
            IQueryable<Subject> query = _entity
                .AsNoTracking()
                .Include(sub => sub.SubjectTeachers)
                    .ThenInclude(t => t.Teacher)
                .Include(sub => sub.SubjectTeachers)
                    .ThenInclude(c => c.Class)
                .Where(searchFilter);
            var orderedQuery = ApplySorting(query, subjectQueryDto.SortBy, subjectQueryDto.SortOrder)
                .ThenBy(sub => sub.SubjectId);

            var totalCount = await orderedQuery.CountAsync();

            var items = await orderedQuery
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
        public static Expression<Func<Subject, bool>> SearchFilter(string? search)
        {
            var query = PredicateBuilder.New<Subject>(false);
            if (string.IsNullOrWhiteSpace(search))
            {
                return sub => true;

            }
            query = query.Or(sub => sub.Name.Contains(search));
            return query;
        }
    }
}
