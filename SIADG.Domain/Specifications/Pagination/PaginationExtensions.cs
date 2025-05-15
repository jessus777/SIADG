using SIADG.Domain.Interfaces;

namespace SIADG.Domain.Specifications.Pagination;

public static class PaginationExtensions
{
    public static CursorPaginationResult<TCursor, TItem> ToCursorPaginationResult<TCursor, TItem>(
        this IEnumerable<TItem> items,
        CursorPaginationOptions<TCursor> paginationOptions
    )
    {
        var cursorType = typeof(TCursor);
        var array = items.ToArray();
        var lastItem = array.LastOrDefault();
        var nextCursor = lastItem is IConsecutiveEntity consecutiveEntity 
            ? (TCursor) Convert.ChangeType(consecutiveEntity.Consecutivo, cursorType)
            : paginationOptions.Cursor;
        
        return new CursorPaginationResult<TCursor, TItem>(array, paginationOptions, nextCursor);
    }
    
    public static CursorPaginationResult<TCursor, TItem> ToCursorPaginationResult<TCursor, TItem>(
        this IEnumerable<TItem> items,
        Func<IEnumerable<TItem>, TCursor> nextCursor,
        CursorPaginationOptions<TCursor> paginationOptions
    )
    {
        var array = items.ToArray();
        return new CursorPaginationResult<TCursor, TItem>(array, paginationOptions, nextCursor(array));
    }
    
    public static CursorPaginationResult<TCursor, TItem> ToCursorPaginationResult<TCursor, TItem>(
        this IEnumerable<TItem> items,
        Func<TItem, TCursor> nextCursor,
        CursorPaginationOptions<TCursor> paginationOptions
    )
    {
        var array = items.ToArray();
        var lastItem = array.LastOrDefault();
        
        return new CursorPaginationResult<TCursor, TItem>(
            array,
            paginationOptions,
            lastItem is not null ? nextCursor(lastItem) : default
            );
    }
}