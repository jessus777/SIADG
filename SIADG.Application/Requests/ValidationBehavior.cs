using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace SIADG.Application.Requests;

public sealed class ValidationBehavior<TRequest, TResult> : IPipelineBehavior<TRequest, TResult>
    where TRequest : notnull
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResult> Handle(TRequest request, RequestHandlerDelegate<TResult> next, CancellationToken cancellationToken)
    {
        if (!_validators.Any())
            return await next();

        var validationContext = new ValidationContext<TRequest>(request);

        var validationResults = new List<ValidationResult>();
        foreach (var validator in _validators)
            validationResults.Add(await validator.ValidateAsync(validationContext, cancellationToken));

        var errors = (
            from e in validationResults.SelectMany(r => r.Errors)
            where e is not null
            select e
        ).ToArray();

        if (errors.Length != 0)
            throw new ValidationException("AlgunosDatosIntroducidosNoSonValidos", errors);

        return await next();
    }
}