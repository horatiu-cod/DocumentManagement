namespace DocumentManagement.Domain.Documents.Dtos;

internal sealed class DocumentForCreationDto
{
    public string Name { get; set; } = string.Empty;
    public Guid OwnerId { get; set; }
}
