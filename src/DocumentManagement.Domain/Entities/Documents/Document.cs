using DocumentManagement.Domain.Entities.Employees;
using DocumentManagement.Domain.Entities.Signatures;

namespace DocumentManagement.Domain.Entities.Documents;
public class Document
{
    public Guid Id {get; set;} = Guid.NewGuid();
    public required string FileName {get; set;}
    public required string StorageFileName {get; set;}
    public required string DocumentType {get; set;}
    public string ContentType { get; set; } = string.Empty;
    public string FileExtension { get; set; } = string.Empty;
    public DateTimeOffset CreatedAt {get; set;}
    public DateTimeOffset LastModifiedAt {get; set;}
    public DateTimeOffset DeletedAt { get; set; }
    public ICollection<Signature>? Signatures { get; set; }
    public Guid OwnerId {get; set;}
    public required Employee Employee { get; set;}
    public byte[] RowVersion { get; set; } = [];
}

