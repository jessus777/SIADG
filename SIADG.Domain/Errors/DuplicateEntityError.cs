namespace SIADG.Domain.Errors;

public sealed class DuplicateEntityError : DomainError
{
    public DuplicateEntityError(object identifier)
    {
        Identifier = identifier;
        Message = "ElementoDuplicado";
        Detail = string.Format("YaExisteElementoX", identifier);
    }

    public object Identifier { get; }
}