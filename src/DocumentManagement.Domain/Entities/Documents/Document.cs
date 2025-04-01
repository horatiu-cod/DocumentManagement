using DocumentManagement.Domain.Entities.Employees;
using DocumentManagement.Domain.Entities.Signatures;
using System.ComponentModel.DataAnnotations;

namespace DocumentManagement.Domain.Entities.Documents;
public class Document
{
    [Key]
    public Guid Id {get; set;} = Guid.NewGuid();
    public required string FileName {get; set;}
    public required string FileUrl {get; set;}
    public required string DocumentType {get; set;}
    public DateTimeOffset CreatedAt {get; set;}
    public DateTimeOffset LastModifiedAt {get; set;}
    public DateTimeOffset DeletedAt { get; set; }
    public ICollection<Signature>? Signatures { get; set; }
    public Guid CreatedBy {get; set;}
    public required Employee Employee { get; set;}
}

