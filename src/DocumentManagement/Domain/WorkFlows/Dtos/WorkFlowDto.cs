using System;

namespace DocumentManagement.Domain.WorkFlows.Dtos;

public sealed record WorkFlowDto
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public int Version { get; init; }
    public string? Status { get; init; } // Assuming WorkFlowStatusEnum is a string representation

    // Add Props Marker -- Deleting this comment will cause the add props utility to be incomplete
}
