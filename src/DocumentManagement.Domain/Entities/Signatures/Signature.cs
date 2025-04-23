using DocumentManagement.Domain.Entities.Documents;
using DocumentManagement.Domain.Entities.Employees;

namespace DocumentManagement.Domain.Entities.Signatures;

public class Signature
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime IssuedAt { get; set; }
    public bool IsValid { get; set; }

    public Guid IssuedBy { get; set; }
    public required Employee Employee { get; set; }

    public Guid IssuedFor { get; set; }
    public required DocumentEntity Document { get; set; }
}
