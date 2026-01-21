using SchoolManager.Dtos.Common;

namespace SchoolManager.Mappers.Common
{
    public static class PagedResultsMapper
    {
        public static PagedResults<T> ToPagedResults<T>(
            this List<T> results,
            int pageNumber,
            int pageSize,
            int totalCount
            )
        {
            return new PagedResults<T>
            {
                Results = results,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount
            };
        }
    }
}
