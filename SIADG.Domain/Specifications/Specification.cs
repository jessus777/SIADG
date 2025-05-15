using System.Linq.Expressions;

namespace SIADG.Domain.Specifications;
public abstract class Specification<T>
{
    // Expresión para el criterio WHERE (EF Core o LINQ)
    public abstract Expression<Func<T, bool>> ToExpression();

    // Método para evaluar en memoria (IEnumerable)
    public bool IsSatisfiedBy(T entity)
        => ToExpression().Compile()(entity);

    // Operadores lógicos (AND, OR, NOT)
    public Specification<T> And(Specification<T> other)
        => new AndSpecification<T>(this, other);

    public Specification<T> Or(Specification<T> other)
        => new OrSpecification<T>(this, other);

    public Specification<T> Not()
        => new NotSpecification<T>(this);
}

public class AndSpecification<T> : Specification<T>
{
    private readonly Specification<T> _left;
    private readonly Specification<T> _right;

    public AndSpecification(Specification<T> left, Specification<T> right)
    {
        _left = left;
        _right = right;
    }

    public override Expression<Func<T, bool>> ToExpression()
    {
        var leftExpr = _left.ToExpression();
        var rightExpr = _right.ToExpression();

        var param = Expression.Parameter(typeof(T));
        var combined = Expression.AndAlso(
            Expression.Invoke(leftExpr, param),
            Expression.Invoke(rightExpr, param)
        );

        return Expression.Lambda<Func<T, bool>>(combined, param);
    }
}
public class OrSpecification<T> : Specification<T>
{
    private readonly Specification<T> _left;
    private readonly Specification<T> _right;
    public OrSpecification(Specification<T> left, Specification<T> right)
    {
        _left = left;
        _right = right;
    }
    public override Expression<Func<T, bool>> ToExpression()
    {
        var leftExpr = _left.ToExpression();
        var rightExpr = _right.ToExpression();
        var param = Expression.Parameter(typeof(T));
        var combined = Expression.OrElse(
            Expression.Invoke(leftExpr, param),
            Expression.Invoke(rightExpr, param)
        );
        return Expression.Lambda<Func<T, bool>>(combined, param);
    }
}
public class NotSpecification<T> : Specification<T>
{
    private readonly Specification<T> _specification;
    public NotSpecification(Specification<T> specification)
    {
        _specification = specification;
    }
    public override Expression<Func<T, bool>> ToExpression()
    {
        var expr = _specification.ToExpression();
        var param = Expression.Parameter(typeof(T));
        var negated = Expression.Not(Expression.Invoke(expr, param));
        return Expression.Lambda<Func<T, bool>>(negated, param);
    }
}
