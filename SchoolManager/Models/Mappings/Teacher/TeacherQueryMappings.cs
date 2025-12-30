using SchoolManager.Models.Dtos.Common;
using SchoolManager.Models.Dtos.Teacher;

namespace SchoolManager.Models.Mappings.Teacher
{
    public static class TeacherQueryMappings
    {
        private const int DefaultPageNumber = 1;
        private const int DefaultPageSize = 20;
        private const int MaxPageSize = 200;

        public static TeacherQueryDto Normalize(this TeacherQueryDto teacherQueryDto)
        {
            var pageNumber = teacherQueryDto?.PageNumber ?? DefaultPageNumber;
            if (pageNumber < 1)
            {
                pageNumber = DefaultPageNumber;
            }

            var pageSize = teacherQueryDto?.PageSize ?? DefaultPageSize;
            if (pageSize < 1)
            {
                pageSize = DefaultPageSize;
            }
            if (pageSize > MaxPageSize)
            {
                pageSize = MaxPageSize;
            }

            var search = teacherQueryDto?.Search?.Trim();
            if (string.IsNullOrWhiteSpace(search))
            {
                search = null;
            }


            var filterBy = teacherQueryDto?.FilterBy ?? TeacherFilterBy.None;


            // Infer filter when caller provides parameters but doesn't set FilterBy explicitly
            if (filterBy == TeacherFilterBy.None && search is not null)
            {
                filterBy = TeacherFilterBy.Search;
            }
            if (filterBy != TeacherFilterBy.Search)
            {
                search = null;
            }

            return new TeacherQueryDto
            {
                FilterBy = filterBy,
                Search = search,
                SortBy = teacherQueryDto?.SortBy ?? TeacherSortBy.Name,
                SortDirection = teacherQueryDto?.SortDirection ?? SortDirection.Asc,
                PageNumber = pageNumber,
                PageSize = pageSize,
            };
        }
    }
}
