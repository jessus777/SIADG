namespace SIADG.Domain.Specifications.Pagination;

public abstract class PaginationResult<TItem>
{
    protected PaginationResult(IEnumerable<TItem> items, PaginationOptions paginationOptions)
    {
        Items = items;
        PageSize = paginationOptions.PageSize ?? Items.Count();
    }

    public IEnumerable<TItem> Items { get; }
    public long PageSize { get; }
}