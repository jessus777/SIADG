namespace SIADG.Domain.Errors
{
    public sealed class EntityInvalidDataError 
        : DomainError
    {
        public EntityInvalidDataError(object identifier, List<string> errors)
        {
            Identifier = identifier;
            Message = "ElementoConflicto";
            Detail = "Error en validación de datos";
            CausedBy(errors);
        }

        public object Identifier { get; }
        public List<string> Errors { get; } = [];
    }
}