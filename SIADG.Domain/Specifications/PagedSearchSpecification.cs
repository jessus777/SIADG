using System.Linq.Expressions;

namespace SIADG.Domain.Specifications;
public abstract class PagedSearchSpecification<T> : Specification<T>
{
    // Paginación
    public int PageNumber { get; }
    public int PageSize { get; }

    // Ordenamiento
    public Expression<Func<T, object>>? OrderBy { get; }
    public bool OrderDescending { get; }

    // Búsqueda (filtros dinámicos)
    public Expression<Func<T, bool>>? Filter { get; }

    // Búsqueda textual (opcional)
    public string? SearchTerm { get; }
    public Expression<Func<T, bool>>? SearchPredicate { get; }

    public PagedSearchSpecification(
        int pageNumber = 1,
        int pageSize = 10,
        Expression<Func<T, object>>? orderBy = null,
        bool orderDescending = false,
        Expression<Func<T, bool>>? filter = null,
        string? searchTerm = null,
        Expression<Func<T, bool>>? searchPredicate = null)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
        OrderBy = orderBy;
        OrderDescending = orderDescending;
        Filter = filter;
        SearchTerm = searchTerm;
        SearchPredicate = searchPredicate;
    }

    // Implementación base para filtrado (usado en Where)
    public override Expression<Func<T, bool>> ToExpression()
    {
        // Si no hay filtros, retorna "true" (sin filtrado)
        if (Filter == null && SearchPredicate == null)
            return x => true;

        // Si solo hay un filtro, retorna ese
        if (Filter == null)
            return SearchPredicate!;
        if (SearchPredicate == null)
            return Filter;

        // Combina Filter AND SearchPredicate usando Expression.AndAlso
        var parameter = Expression.Parameter(typeof(T), "x");
        var combined = Expression.AndAlso(
            Expression.Invoke(Filter, parameter),
            Expression.Invoke(SearchPredicate, parameter)
        );

        return Expression.Lambda<Func<T, bool>>(combined, parameter);
    }

    // Aplica paginación y ordenamiento a un IQueryable
    public IQueryable<T> ApplyPaging(IQueryable<T> query)
    {
        if (OrderBy != null)
        {
            query = OrderDescending
                ? query.OrderByDescending(OrderBy)
                : query.OrderBy(OrderBy);
        }

        return query
            .Where(ToExpression())
            .Skip((PageNumber - 1) * PageSize)
            .Take(PageSize);
    }

}
