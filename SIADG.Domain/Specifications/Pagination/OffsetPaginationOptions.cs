namespace SIADG.Domain.Specifications.Pagination;

public sealed class OffsetPaginationOptions 
    : PaginationOptions
{
    public OffsetPaginationOptions(long pageNumber, long pageSize) : base(pageSize)
    {
        PageNumber = pageNumber;
    }

    public long PageNumber { get; }

    public void Translate(out long offset, out long limit)
    {
        offset = (PageNumber - 1) * PageSize!.Value;
        limit = PageSize!.Value;
    }
}