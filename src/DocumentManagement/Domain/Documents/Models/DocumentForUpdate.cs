namespace DocumentManagement.Domain.Documents.Models;

internal sealed record DocumentForUpdate
{
    public bool IsSigned { get; set; }
    public Guid SignedBy { get; }
    public DateTimeOffset SignedAt { get; }
    public DateTimeOffset TransmittedAt { get; set; }
    public string TransmittedTo { get; set; } = string.Empty;
    public DateTimeOffset? AllocatedAt { get;  set; }
    public Guid AllocatedTo { get; set; }
    public string Status { get; set; } = string.Empty;
}
