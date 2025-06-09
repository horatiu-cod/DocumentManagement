using DocumentManagement.Domain.Documents.Dtos;
using DocumentManagement.Domain.Documents.Models;

namespace DocumentManagement.Domain.Documents.Mappings;

internal static class DocumentMapper
{
    public static DocumentDto ToDocumentDto(this Document document)
    {
        return new DocumentDto
        {
            Id = document.Id,
            Name = document.Name,
            OwnerId = document.OwnerId,
            IsSigned = document.IsSigned,
            SignedBy = document.SignedBy ?? Guid.Empty,
            SignedAt = document.SignedAt ?? DateTimeOffset.MinValue,
            TransmittedAt = document.TransmittedAt ?? DateTimeOffset.MinValue,
            TransmittedTo = document.TransmittedTo ?? string.Empty,
            AllocatedAt = document.AllocatedAt,
            AllocatedTo = document.AllocatedTo,
            Status = document.DocumentStatus.Value
        };
    }
    public static DocumentForCreation ToDocumentForCreation(this DocumentForCreationDto documentDto)
    {
        return new DocumentForCreation
        {
            Name = documentDto.Name,
            OwnerId = documentDto.OwnerId
        };
    }
}
