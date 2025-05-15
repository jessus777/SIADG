namespace SIADG.Domain.Specifications.Pagination;

public abstract class PaginationOptions
{
    protected PaginationOptions(long? pageSize)
    {
        PageSize = pageSize;
    }

    public long? PageSize { get; }

    public long ResolvePageSize(long maxPageSize)
        => PageSize <= maxPageSize ? PageSize.Value : maxPageSize;
}