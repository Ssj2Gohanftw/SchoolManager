namespace SchoolManager.Dtos.Common
{
    public class PagedResults<T>
    {
        public required List<T> Results { get; init; }
        public required int PageNumber { get; init; }
        public required int PageSize { get; init; }
        public required int TotalCount { get; init; }

    }
}
