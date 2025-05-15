using FluentResults;
using MediatR;

namespace SIADG.Application.Requests;

public abstract class ApplicationRequestHandler<TRequest, TResult> 
    : IRequestHandler<TRequest, Result<TResult>>
    where TRequest : ApplicationRequest<TResult>
{
    Task<Result<TResult>> IRequestHandler<TRequest, Result<TResult>>.Handle(
        TRequest request,
        CancellationToken cancellationToken
        )
        => HandleAsync(request, cancellationToken);

    protected abstract Task<Result<TResult>> HandleAsync(TRequest request, CancellationToken cancellationToken);
    
    protected Result<TResult> Ok(TResult value) => Result.Ok(value);
}

public abstract class ApplicationRequestHandler<TRequest> 
    : IRequestHandler<TRequest, Result>
    where TRequest : ApplicationRequest
{
    Task<Result> IRequestHandler<TRequest, Result>.Handle(
        TRequest request,
        CancellationToken cancellationToken
        )
        => HandleAsync(request, cancellationToken);

    protected abstract Task<Result> HandleAsync(TRequest request, CancellationToken cancellationToken);

    protected Result Ok() => Result.Ok();
}