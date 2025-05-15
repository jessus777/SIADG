namespace SIADG.Domain.Errors;

public sealed class EntityNotFoundError 
    : DomainError
{
    public EntityNotFoundError(object identifier)
    {
        Identifier = identifier;
        Message = "ElementoNoEncontrado";
        Detail = string.Format("NoSeEncontroElementoConId", identifier);
    }

    public object Identifier { get; }
}