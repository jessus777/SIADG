namespace SIADG.Domain.Specifications.Pagination;

public sealed class CursorPaginationOptions<TCursor> : PaginationOptions
{
    public CursorPaginationOptions(TCursor? cursor, long? pageSize = null) : base(pageSize)
    {
        Cursor = cursor;
    }

    public TCursor? Cursor { get; }
}