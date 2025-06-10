namespace DocumentManagement.Domain.Documents.Models;

internal sealed record DocumentForCreation
{
    public string Name { get; set; } = string.Empty;
    public Guid OwnerId { get; set; }
}
