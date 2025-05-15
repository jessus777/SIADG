namespace SIADG.Domain.Errors;

public sealed class EntityConflictError : DomainError
{
    public EntityConflictError(object identifier, string detail)
    {
        Identifier = identifier;
        Message = "ElementoConflicto";
        Detail = detail;
    }

    public object Identifier { get; }
}