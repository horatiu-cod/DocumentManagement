using DocumentManagement.Utilities;

namespace DocumentManagement.Domain.WorkFlows.Services;

public interface IWorkFlowService
{
    
}

public class WorkFlowService
{
    public static Result<bool> CanGoNext() => Result<bool>.Success(true);

    public static Result GoNext(WorkFlow workFlow)
    {
        var actualStep = workFlow.Steps.
    }
}
