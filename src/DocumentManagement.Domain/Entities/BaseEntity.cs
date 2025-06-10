using System.ComponentModel.DataAnnotations;

namespace DocumentManagement.Domain.Entities;

public abstract class BaseEntity
{
    [Key]
    public Guid Id { get; private set; } = Guid.NewGuid();
    public DateTimeOffset CreatedOn { get; private set; }
    public Guid CreatedBy { get; private set; }
    public DateTimeOffset? LastModifiedOn { get; private set; }
    public Guid LastModifiedBy { get; private set; }
    public bool IsDeleted { get; private set; }

    public void UpdateCreationProperties(DateTimeOffset createdOn, Guid createdBy)
    {
        CreatedOn = createdOn;
        CreatedBy = createdBy;
    }

    public void UpdateModifiedProperties(DateTimeOffset? lastModifiedOn, Guid lastModifiedBy)
    {
        LastModifiedOn = lastModifiedOn;
        LastModifiedBy = lastModifiedBy;
    }

    public void UpdateIsDeleted(bool isDeleted)
    {
        IsDeleted = isDeleted;
    }

    public void OverrideId(Guid id)
    {
        Id = id;
    }
}
