namespace DocumentManagement.Domain.Documents.Dtos;

internal sealed class DocumentDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public Guid OwnerId { get; set; }
    public bool IsSigned { get; set; }
    public Guid SignedBy { get; set; }
    public DateTimeOffset SignedAt { get; set; }
    public DateTimeOffset TransmittedAt { get; set; }
    public string TransmittedTo { get; set; } = string.Empty;
    public DateTimeOffset? AllocatedAt { get; set; }
    public Guid AllocatedTo { get; set; }
    public string Status { get; set; } = string.Empty;
}
