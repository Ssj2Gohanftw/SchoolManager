using LinqKit;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SchoolManager.Data.Repositories.Interfaces;
using SchoolManager.Dtos.Common;
using SchoolManager.Dtos.Student;
using SchoolManager.Mappers.Students;
using SchoolManager.Models.Entities;
using System.Linq.Expressions;

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
            IOrderedQueryable<Student> orderedQuery;
            //This is a switch statement . Here we mutate the query and return it
            switch (sortBy, desc)
            {
                case (StudentSortBy.LastName, false):
                    orderedQuery= query.OrderBy(s => s.LastName);
                    break;

                case (StudentSortBy.LastName, true):
                    orderedQuery = query.OrderByDescending(s => s.LastName);
                    break;
                case (StudentSortBy.Email, false):
                    orderedQuery = query.OrderBy(s => s.Email);
                    break;
                case (StudentSortBy.Email, true):
                    orderedQuery = query.OrderByDescending(s => s.Email);
                    break;
                case (StudentSortBy.DateOfBirth, false):
                    orderedQuery = query.OrderBy(s => s.DateOfBirth);
                    break;
                case (StudentSortBy.DateOfBirth, true):
                    orderedQuery = query.OrderByDescending(s => s.DateOfBirth);
                    break;
                case (StudentSortBy.ClassName, false):
                    orderedQuery = query.OrderBy(s => s.Class!.Name);
                    break;
                case (StudentSortBy.ClassName, true):
                    orderedQuery = query.OrderByDescending(s => s.Class!.Name);
                    break;
                case (StudentSortBy.FirstName, false):
                    orderedQuery = query.OrderBy(s => s.FirstName);
                    break;
                default:
                    orderedQuery = query.OrderByDescending(s => s.FirstName);
                    break;
            }
            return (orderedQuery);

            //This is a switch exp,it returns a value and we don't mutate the query.

            //return (sortBy, desc) switch
            //{
            //    (StudentSortBy.LastName, false) => query.OrderBy(s => s.LastName),
            //    (StudentSortBy.LastName, true) => query.OrderByDescending(s => s.LastName),

            //    (StudentSortBy.Email, false) => query.OrderBy(s => s.Email),
            //    (StudentSortBy.Email, true) => query.OrderByDescending(s => s.Email),

            //    (StudentSortBy.DateOfBirth, false) => query.OrderBy(s => s.DateOfBirth),
            //    (StudentSortBy.DateOfBirth, true) => query.OrderByDescending(s => s.DateOfBirth),

            //    (StudentSortBy.ClassName, false) => query.OrderBy(s => s.Class!.Name),
            //    (StudentSortBy.ClassName, true) => query.OrderByDescending(s => s.Class!.Name),

            //    (StudentSortBy.FirstName, true) => query.OrderByDescending(s => s.FirstName),
            //    _ => query.OrderBy(s => s.FirstName)
            //};
        }

        public async Task<PagedResults<Student>> GetPagedAsync(StudentQueryDto studentQueryDto)
        {
            studentQueryDto = studentQueryDto.Normalize();
            var searchFilter = SearchFilter(studentQueryDto.Search);
                //.Expand();
            IQueryable<Student> query = _entity
                .AsNoTracking()
                .AsExpandableEFCore()
                .Include(s => s.Class)
                .Include(s => s.StudentSubjects)
                    .ThenInclude(s => s.Subject);

           
            query = query.Where(searchFilter);

            var orderedQuery= ApplySorting(query, studentQueryDto.SortBy, studentQueryDto.SortOrder)
                .ThenBy(s => s.StudentId);

            var totalCount = await orderedQuery.CountAsync();

            var results = await orderedQuery
                .Skip((studentQueryDto.PageNumber - 1) * studentQueryDto.PageSize)
                .Take(studentQueryDto.PageSize)
                .ToListAsync();



            return new PagedResults<Student>
            {
                Results = results,
                PageNumber = studentQueryDto.PageNumber,
                PageSize = studentQueryDto.PageSize,
                TotalCount = totalCount
            };
        }
       public static Expression<Func<Student,bool>> SearchFilter(string? search)
        {
            if (string.IsNullOrWhiteSpace(search))
            {
                return PredicateBuilder.New<Student>(true);

            }
            var query = PredicateBuilder.New<Student>(false);

            query = query.Or(s => s.LastName.Contains(search));
            query = query.Or(s =>  s.FirstName.Contains(search));
            //query = query.Or(s => s.FirstName.Contains(search)).And(s => s.LastName.Contains(search));
            query = query.Or(s => s.Email.Contains(search));
           // query = query.Or(s =>
           // s.AdditionalInfo != null &&
           // s.AdditionalInfo.Hobbies != null &&
           // s.AdditionalInfo.Hobbies.Any(hobby => hobby.Contains(search))
           //);

            var fullName = search.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if(fullName.Length >= 2)
            {
                string fname = fullName[0];
                string lname = fullName[1];
                query = query.Or(s => s.FirstName.Contains(fname) && s.LastName.Contains(lname));
                query = query.Or(s => s.FirstName.Contains(lname) && s.LastName.Contains(fname));
            }

            return query;
        }

    }
}
