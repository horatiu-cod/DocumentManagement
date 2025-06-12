using DocumentManagement.Cqrs.Commands;
using DocumentManagement.Domain.WorkFlows.Dtos;
using DocumentManagement.Domain.WorkFlows.Mappings;
using Microsoft.Extensions.Logging;

namespace DocumentManagement.Domain.WorkFlows.Features;

public static class AddWorkFlow
{
    public sealed record Command(
        WorkFlowForCreationDto WorkFlowsToAdd,
        Guid UserId
    ) : ICommand<WorkFlowDto>;

    public sealed class Handler : ICommandHandler<Command, WorkFlowDto>
    {
       private readonly ILogger<Handler> _logger;
      
        public Handler(ILogger<Handler> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task<WorkFlowDto> HandleAsync(Command command, CancellationToken cancellationToken)
        {
            var workFlowToAdd = command.WorkFlowsToAdd.ToWorkFlowsForCreation();
            var workFlow = WorkFlow.Create(
                workFlowToAdd,
                command.UserId
            );
            await Task.Yield();
            _logger.LogInformation("Adding new workflow: {@WorkFlow}", workFlowToAdd);

            // Here you would typically save the workFlow to a database or repository.
            // For demonstration purposes, we will just return the mapped DTO.
            var workFlowDto = workFlow.ToWorkFlowDto();
            _logger.LogInformation("Workflow added successfully: {@WorkFlowDto}", workFlowDto);

            return workFlowDto;
        }
    }
}
