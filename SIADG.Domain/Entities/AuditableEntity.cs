namespace SIADG.Domain.Entities;
public abstract class AuditableEntity<TId>
    : Entity<TId>
{
    public DateTime? Created { get; protected set; }
    public Guid? CreatedBy { get; protected set; }
    public DateTime? LastModifiedAt { get; protected set; }
    public Guid? ModifiedBy { get; protected set; }

    protected AuditableEntity(TId id) : base(id) { }
    protected AuditableEntity() { }
}
