using SchoolManager.Models.Dtos.Class;
using SchoolManager.Models.Dtos.Common;

namespace SchoolManager.Models.Mappings.Class
{
    public static class ClassQueryMappings
    {
        private const int DefaultPageNumber = 1;
        private const int DefaultPageSize = 20;
        private const int MaxPageSize = 200;

        public static ClassQueryDto Normalize(this ClassQueryDto classQueryDto)
        {
            var pageNumber = classQueryDto?.PageNumber ?? DefaultPageNumber;
            if (pageNumber < 1)
            {
                pageNumber = DefaultPageNumber;
            }

            var pageSize = classQueryDto?.PageSize ?? DefaultPageSize;
            if (pageSize < 1)
            {
                pageSize = DefaultPageSize;
            }
            if (pageSize > MaxPageSize)
            {
                pageSize = MaxPageSize;
            }

            var search = classQueryDto?.Search?.Trim();
            if (string.IsNullOrWhiteSpace(search))
            {
                search = null;
            }


            var filterBy = classQueryDto?.FilterBy ?? ClassFilterBy.None;


            // Infer filter when caller provides parameters but doesn't set FilterBy explicitly
            if (filterBy == ClassFilterBy.None && search is not null)
            {
                filterBy = ClassFilterBy.Search;
            }
            if (filterBy !=ClassFilterBy.Search)
            {
                search=null;
            }

            return new ClassQueryDto
            {
                FilterBy = filterBy,
                Search = search,
                SortBy = classQueryDto?.SortBy ?? ClassSortBy.Name,
                SortDirection = classQueryDto?.SortDirection ?? SortDirection.Asc,
                PageNumber = pageNumber,
                PageSize = pageSize,
            };
        }
    }
}
