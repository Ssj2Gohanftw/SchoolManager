using SchoolManager.Models.Dtos.Class;
using SchoolManager.Models.Dtos.Common;
using SchoolManager.Models.Dtos.Subject;

namespace SchoolManager.Models.Mappings.Subject
{
    public static class SubjectQueryMappings
    {
        private const int DefaultPageNumber = 1;
        private const int DefaultPageSize = 20;
        private const int MaxPageSize = 200;

        public static SubjectQueryDto Normalize(this SubjectQueryDto subjectQueryDto)
        {
            var pageNumber = subjectQueryDto?.PageNumber ?? DefaultPageNumber;
            if (pageNumber < 1)
            {
                pageNumber = DefaultPageNumber;
            }

            var pageSize = subjectQueryDto?.PageSize ?? DefaultPageSize;
            if (pageSize < 1)
            {
                pageSize = DefaultPageSize;
            }
            if (pageSize > MaxPageSize)
            {
                pageSize = MaxPageSize;
            }

            var search = subjectQueryDto?.Search?.Trim();
            if (string.IsNullOrWhiteSpace(search))
            {
                search = null;
            }


            var filterBy = subjectQueryDto?.FilterBy ?? SubjectFilterBy.None;


            // Infer filter when caller provides parameters but doesn't set FilterBy explicitly
            if (filterBy == SubjectFilterBy.None && search is not null)
            {
                filterBy = SubjectFilterBy.Search;
            }
            if (filterBy != SubjectFilterBy.Search)
            {
                search = null;
            }

            return new SubjectQueryDto
            {
                FilterBy = filterBy,
                Search = search,
                SortBy = subjectQueryDto?.SortBy ?? SubjectSortBy.Name,
                SortDirection = subjectQueryDto?.SortDirection ?? SortDirection.Asc,
                PageNumber = pageNumber,
                PageSize = pageSize,
            };
        }
    }
}
