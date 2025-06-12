using DocumentManagement.Domain.WorkFlows.Dtos;
using DocumentManagement.Domain.WorkFlows.Models;

namespace DocumentManagement.Domain.WorkFlows.Mappings;

internal static class WorkFlowMapper
{
    public static WorkFlowDto ToWorkFlowDto(this WorkFlow workFlow)
    {
        return new WorkFlowDto
        {
            Id = workFlow.Id,
            Name = workFlow.Name,
            Version = workFlow.Version,
            Status = workFlow.Status.Name // Assuming WorkFlowStatusEnum is a string representation
        };
    }

    public static WorkFlowsForCreation ToWorkFlowsForCreation(this WorkFlowForCreationDto workFlowDto)
    {
        return new WorkFlowsForCreation(workFlowDto.Name, workFlowDto.Version);
    }
}
