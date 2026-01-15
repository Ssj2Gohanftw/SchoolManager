using SchoolManager.Dtos.Common;

namespace SchoolManager.Extensions
{
    public static class PaginationExtensions
    {
        private const int DefaultPageNumber = 1;
        private const int DefaultPageSize = 20;
        private const int MaxPageSize = 200;

        //A Generic method for pagination normalization for the entity query dtos
        public static TQueryDto QueryNormalize<TQueryDto>
            (
            this TQueryDto dto,
            Func<TQueryDto,int,int,FilterBy,string?,TQueryDto> toEntityQueryDto
            ) where TQueryDto : QueryDto
        {
            ArgumentNullException.ThrowIfNull(dto);
            var pageNumber = dto?.PageNumber ?? DefaultPageNumber;
            if (pageNumber < 1)
            {
                pageNumber = DefaultPageNumber;
            }

            var pageSize = dto?.PageSize ?? DefaultPageSize;
            if (pageSize < 1)
            {
                pageSize = DefaultPageSize;
            }
            if (pageSize > MaxPageSize)
            {
                pageSize = MaxPageSize;
            }

            var search = dto?.Search?.Trim();
            if (string.IsNullOrWhiteSpace(search))
            {
                search = null;
            }


            var filterBy = dto?.FilterBy ?? FilterBy.None;

            if (filterBy == FilterBy.None && search !=null)
            {
                filterBy = FilterBy.Search;
            }
            if (filterBy != FilterBy.Search)
            {
                search = null;
            }
            return toEntityQueryDto(dto,pageNumber, pageSize,filterBy, search);
        }
    }
}
