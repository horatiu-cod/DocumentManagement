using DocumentManagement.Domain.Documents.Models;
using DocumentManagement.Domain.DocumentStatuses;

namespace DocumentManagement.Domain.Documents;

internal sealed class Document : BaseEntity
{
    public string Name { get; private set; } = string.Empty;
    public bool Editable { get; private set; } = true;
    public Guid OwnerId => CreatedBy; // Assuming CreatedBy is the owner of the address
    public bool IsSigned { get; private set; } = false;
    public Guid? SignedBy { get; private set; } 
    public DateTimeOffset? SignedAt { get; private set; }
    public DateTimeOffset? TransmittedAt { get; private set; }
    public string TransmittedTo { get; private set; } = string.Empty;
    public DateTimeOffset? AllocatedAt { get; private set;  }
    public Guid AllocatedTo { get; private set; }
    public DocumentStatus DocumentStatus { get; private set; } = DocumentStatus.Created();


    public static Document Create(DocumentForCreation documentForCreation)
    {
        var newDocument = new Document();
        newDocument.Name = documentForCreation.Name;
        newDocument.AllocatedAt = DateTimeOffset.UtcNow;
        newDocument.AllocatedTo = documentForCreation.OwnerId;
        newDocument.SignedBy = documentForCreation.OwnerId;
        newDocument.UpdateCreationProperties(DateTimeOffset.UtcNow, documentForCreation.OwnerId);

        return newDocument;
    }

    public Document Update(DocumentForUpdate documentForUpdate)
    {
        if (documentForUpdate.IsSigned)
        {
            IsSigned = true;
            SignedBy = documentForUpdate.SignedBy;
            SignedAt = documentForUpdate.SignedAt;
        }
        TransmittedAt = documentForUpdate.TransmittedAt;
        TransmittedTo = documentForUpdate.TransmittedTo;
        AllocatedAt = documentForUpdate.AllocatedAt;
        AllocatedTo = documentForUpdate.AllocatedTo;
        DocumentStatus = DocumentStatus.Of(documentForUpdate.Status);

        UpdateModifiedProperties(DateTimeOffset.UtcNow, documentForUpdate.SignedBy);
        return this;
    }
}
