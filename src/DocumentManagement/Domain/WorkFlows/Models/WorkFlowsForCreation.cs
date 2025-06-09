using DocumentManagement.Domain.Steps;

namespace DocumentManagement.Domain.WorkFlows.Models;

internal sealed record WorkFlowsForCreation
{
    public string Name { get; set; }
    //public List<Step> Steps { get; set; } = new();
}
