using SIADG.Domain.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIADG.Domain.Entities;
public abstract class Entity<TId> : IEquatable<Entity<TId>>
    where TId : notnull
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Opcional para EF Core
    public TId Id { get; protected set; } // Protected para inmutabilidad

    private readonly List<IDomainEvent> _domainEvents = new();

    [NotMapped] // No persistir en DB
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    protected Entity(TId id) => Id = id;

    // Para EF Core (constructor protegido sin parámetros)
    protected Entity() { }

    public void AddDomainEvent(IDomainEvent eventItem) => _domainEvents.Add(eventItem);
    public void ClearDomainEvents() => _domainEvents.Clear();
    public void RemoveDomainEvent(IDomainEvent eventItem) => _domainEvents.Remove(eventItem);

    // Sobrescribir igualdad (comparar por ID)
    public override bool Equals(object? obj) => obj is Entity<TId> entity && Id.Equals(entity.Id);
    public bool Equals(Entity<TId>? other) => Equals((object?)other);
    public static bool operator ==(Entity<TId>? left, Entity<TId>? right) => Equals(left, right);
    public static bool operator !=(Entity<TId>? left, Entity<TId>? right) => !Equals(left, right);
    public override int GetHashCode() => Id.GetHashCode();
}
