using DocumentManagement.Domain.Entities.Documents;
using DocumentManagement.Domain.Entities.Signatures;

namespace DocumentManagement.Domain.Entities.Employees;

public class Employee
{
    public Guid Id { get; set; }
    public required string Email { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public ICollection<Document>? Documents { get; set; }
    public ICollection<Signature>? Signatures { get; set; }
}
