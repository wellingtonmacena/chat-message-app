namespace posterr_webapi.src.Shared
{
    public interface IPaginate<T>
    {
        IEnumerable<T> Items { get; }
        int TotalCount { get; }
        int PageSize { get; }
        int CurrentPage { get; }
        int TotalPages { get; }
    }
}
