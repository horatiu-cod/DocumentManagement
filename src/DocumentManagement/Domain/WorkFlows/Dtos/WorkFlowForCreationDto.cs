namespace DocumentManagement.Domain.WorkFlows.Dtos;

public sealed record WorkFlowForCreationDto
{
    public string Name { get; init; } = string.Empty;
    public int Version { get; init; }

    // Add Props Marker -- Deleting this comment will cause the add props utility to be incomplete  
}
