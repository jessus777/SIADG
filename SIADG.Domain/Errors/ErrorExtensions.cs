using FluentResults;

namespace SIADG.Domain.Errors;

public static class ErrorExtensions
{
    private static readonly Type DomainErrorType = typeof(DomainError);

    public static bool IsDomainError(this IError error) => IsDomainError(error.GetType());
    public static bool IsDomainError(this Type type) => DomainErrorType.IsAssignableFrom(type);

    public static DomainError? ToDomainError(this IError? error) => error as DomainError;

    public static IEnumerable<DomainError> ToDomainErrors(this IEnumerable<IError> errors)
        => errors.Where(e => e.IsDomainError()).Cast<DomainError>();
}