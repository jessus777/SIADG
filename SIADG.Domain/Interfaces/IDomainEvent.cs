namespace SIADG.Domain.Interfaces;
public interface IDomainEvent
{
    Guid EventId { get; }         // Identificador único del evento
    DateTime OccurredOn { get; }
    string AggregateId { get; }   // ID del agregado que disparó el evento
    string AggregateType { get; } // Tipo del agregado (ej: "User", "Order"
}
