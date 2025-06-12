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

        var actualStep = workFlow.Steps.Where(s => s.StepIndex != 0).Select(s => s).Aggregate((current, next) => current.StepIndex < next.StepIndex ? current : next);
        if (actualStep == null)
            return Result.Fail("No valid next step found in workflow.");

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
        if (nextStep == null)
        {
            return Result.Fail("Next step is null.");
        }
        var newWorkFlow = workFlow.RemoveStepAssignment(nextStep);
        nextStep.UpdateStepCount(-nextStep.StepIndex);
        newWorkFlow = newWorkFlow.AddstepAssignment(nextStep);
        if (newWorkFlow == null)
        {
            return Result.Fail("Failed to remove step from workflow.");
        }

        return Result<WorkFlow>.Success(newWorkFlow);
    }
}
