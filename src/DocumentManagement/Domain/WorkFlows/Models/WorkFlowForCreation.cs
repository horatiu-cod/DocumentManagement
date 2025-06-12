namespace DocumentManagement.Domain.WorkFlows.Models;

internal sealed record WorkFlowsForCreation(string Name, int Version)
{
    // Add Props Marker -- Deleting this comment will cause the add props utility to be incomplete
    // This record is used for creating new workflows, encapsulating the necessary properties.
};
