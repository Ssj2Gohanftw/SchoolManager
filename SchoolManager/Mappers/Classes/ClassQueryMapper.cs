using SchoolManager.Dtos.Class;
using SchoolManager.Dtos.Common;

namespace SchoolManager.Mappers.Classes
{
    public static class ClassQueryMapper
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


            var filterBy = classQueryDto?.FilterBy ?? FilterBy.None;


            // Infer filter when caller provides parameters but doesn't set FilterBy explicitly
            if (filterBy == FilterBy.None && search != null)
            {
                filterBy = FilterBy.Search;
            }
            if (filterBy != FilterBy.Search)
            {
                search = null;
            }

            return classQueryDto.ToClassQueryDto(pageNumber, pageSize, filterBy, search);
        }
    }
}
