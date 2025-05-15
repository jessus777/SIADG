using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SIADG.Domain.Specifications;
public abstract class PagedSpecification<T> 
    : Specification<T>
{
    public int PageNumber { get; }
    public int PageSize { get; }
    public Expression<Func<T, object>> OrderBy { get; }
    public bool OrderDescending { get; }

    public PagedSpecification(
        int pageNumber,
        int pageSize,
        Expression<Func<T, object>> orderBy,
        bool orderDescending = false)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
        OrderBy = orderBy;
        OrderDescending = orderDescending;
    }

    public override Expression<Func<T, bool>> ToExpression()
        => throw new NotSupportedException("PagedSpecification no filtra, solo ordena y pagina.");

    public IQueryable<T> ApplyPaging(IQueryable<T> query)
    {
        var orderedQuery = OrderDescending
            ? query.OrderByDescending(OrderBy)
            : query.OrderBy(OrderBy);

        return orderedQuery.Skip((PageNumber - 1) * PageSize).Take(PageSize);
    }
}
