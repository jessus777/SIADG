using FluentResults;
using MediatR;
using SIADG.Domain;
using System.Text.Json.Serialization;


namespace SIADG.Application.Requests;

public abstract class ApplicationRequest<TResult>
    : IRequest<Result<TResult>>
{
    [JsonIgnore]
    public OperationContext Context { get; internal set; } = null!;
}

public abstract class ApplicationRequest : IRequest<Result>
{
    [JsonIgnore]
    public OperationContext Context { get; internal set; } = null!;
}