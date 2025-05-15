using System.Linq.Expressions;

namespace SIADG.Domain.Specifications;
public static class SpecificationExtensions
{
    public static Expression<Func<T, bool>> And<T>(
        this Expression<Func<T, bool>> left,
        Expression<Func<T, bool>> right)
    {
        var parameter = Expression.Parameter(typeof(T), "x");
        var combined = Expression.AndAlso(
            Expression.Invoke(left, parameter),
            Expression.Invoke(right, parameter)
        );
        return Expression.Lambda<Func<T, bool>>(combined, parameter);
    }
}