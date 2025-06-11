using DocumentManagement.Domain.Steps;
using DocumentManagement.Utilities;

namespace DocumentManagement.Domain.WorkFlows.Services;

internal interface IWorkFlowService
{
    Result<WorkFlow> GoNextStep(WorkFlow workFlow);
}

internal class WorkFlowService : IWorkFlowService
{
    public static Result<bool> CanGoNext() => Result<bool>.Success(true);

    internal static Result<StepsAssignment> GetNextStep(WorkFlow workFlow)
    {
        if (workFlow.Steps == null || workFlow.Steps.Count == 0)
            return Result.Fail("No steps in workflow.");

        if(workFlow.Steps.Count == 1)
            return Result<StepsAssignment>.Success(workFlow.Steps.First());

        var actualStep = workFlow.Steps.Aggregate((current, next) => current.StepCount <  next.StepCount ? current : next);

        return Result<StepsAssignment>.Success(actualStep);
    }

    public Result<WorkFlow> GoNextStep(WorkFlow workFlow)
    {
        var nextStepResult = GetNextStep(workFlow);
        if (!nextStepResult.IsSuccess)
        {
            return Result.Fail(nextStepResult.Error!);
        }
        var nextStep = nextStepResult.Content ;
        var newWorkFlow = workFlow.RemoveStep(nextStep?.Step!);
        if (newWorkFlow == null)
        {
            return Result.Fail("Failed to remove step from workflow.");
        }
        return Result<WorkFlow>.Success(newWorkFlow);
    }
}
