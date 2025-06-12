using DocumentManagement.Cqrs.Commands;
using DocumentManagement.Domain.WorkFlows.Dtos;
using DocumentManagement.Domain.WorkFlows.Mappings;
using Microsoft.Extensions.Logging;

namespace DocumentManagement.Domain.WorkFlows.Features;

public static class AddWorkFlow
{
    public record AddWorkFlowCommand(
        WorkFlowForCreationDto WorkFlowsForCreation,
        Guid UserId
    ) : ICommand<WorkFlowDto>;

    public class AddWorkFlowCommandHandler : ICommandHandler<AddWorkFlowCommand, WorkFlowDto>
    {
       private readonly ILogger<AddWorkFlowCommandHandler> _logger;
      
        public AddWorkFlowCommandHandler(ILogger<AddWorkFlowCommandHandler> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public Task<WorkFlowDto> HandleAsync(AddWorkFlowCommand command, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
