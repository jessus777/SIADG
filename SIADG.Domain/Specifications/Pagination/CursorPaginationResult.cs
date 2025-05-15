namespace SIADG.Domain.Specifications.Pagination;

public sealed class CursorPaginationResult<TCursor, TItem> : PaginationResult<TItem>
{
    public CursorPaginationResult(IEnumerable<TItem> items, CursorPaginationOptions<TCursor> paginationOptions, TCursor? nextCursor)
        : base(items, paginationOptions)
    {
        CurrentCursor = paginationOptions.Cursor;
        NextCursor = nextCursor;
    }
    
    public TCursor? CurrentCursor { get; }
    public TCursor? NextCursor { get; }
}