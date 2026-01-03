using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SchoolManager.Dtos.Common;
using SchoolManager.Dtos.Student;
namespace SchoolManager.Mappers.Students
{
    public static class StudentQueryMapper
    {
        private const int DefaultPageNumber = 1;
        private const int DefaultPageSize = 20;
        private const int MaxPageSize = 200;

        public static StudentQueryDto Normalize(this StudentQueryDto studentQueryDto)
        {
            var pageNumber = studentQueryDto?.PageNumber ?? DefaultPageNumber;
            if (pageNumber < 1)
            {
                pageNumber = DefaultPageNumber;
            }

            var pageSize = studentQueryDto?.PageSize ?? DefaultPageSize;
            if (pageSize < 1)
            {
                pageSize = DefaultPageSize;
            }
            if (pageSize > MaxPageSize)
            {
                pageSize = MaxPageSize;
            }

            var search = studentQueryDto?.Search?.Trim();
            if (string.IsNullOrWhiteSpace(search))
            {
                search = null;
            }


            var classId = studentQueryDto?.ClassId;
            var filterBy = studentQueryDto?.FilterBy ?? StudentFilterBy.None;


            // Infer filter when caller provides parameters but doesn't set FilterBy explicitly
            if (filterBy == StudentFilterBy.None)
            {
                if (classId is not null)
                {
                    filterBy = StudentFilterBy.ClassId;
                }
                else if (search is not null)
                {
                    filterBy = StudentFilterBy.Search;
                }
            }

            // Keep filter inputs consistent with selected filter option
            if (filterBy != StudentFilterBy.Search)
            {
                search = null;
            }

            if (filterBy != StudentFilterBy.ClassId)
            {
                classId = null;
            }

            return new StudentQueryDto
            {
                FilterBy = filterBy,
                Search = search,
                ClassId = classId,
                SortBy = studentQueryDto?.SortBy ?? StudentSortBy.FirstName,
                SortOrder = studentQueryDto?.SortOrder ?? SortOrder.Ascending,
                PageNumber = pageNumber,
                PageSize = pageSize,
            };
        }
    }
}
