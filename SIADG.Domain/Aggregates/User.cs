using Microsoft.AspNetCore.Identity;

namespace SIADG.Domain.Aggregates;
// Fix: Use composition instead of multiple inheritance
public class User : IdentityUser<Guid>
{
    public DateTime? Created { get; private set; }
    public Guid? CreatedBy { get; private set; }
    public DateTime? LastModifiedAt { get; private set; }
    public Guid? ModifiedBy { get; private set; }

    public void SetAuditInfo(DateTime? created, Guid? createdBy, DateTime? lastModifiedAt, Guid? modifiedBy)
    {
        Created = created;
        CreatedBy = createdBy;
        LastModifiedAt = lastModifiedAt;
        ModifiedBy = modifiedBy;
    }


}
