using DocumentManagement.Domain.Steps;
using DocumentManagement.Domain.WorkFlows.Models;
using DocumentManagement.Domain.WorkFlowStatuses;

namespace DocumentManagement.Domain.WorkFlows;

internal class WorkFlow : BaseEntity
{
    public string Name { get; private set; } = string.Empty;
    public int Version { get; private set; }
    public WorkFlowStatusEnum Status { get; private set; } = WorkFlowStatusEnum.Inactive;

    private readonly List<StepsAssignment> _steps = [];
    public IReadOnlyCollection<StepsAssignment> Steps => _steps.AsReadOnly();

    // Add Props Marker -- Deleting this comment will cause the add props utility to be incomplete

    public static WorkFlow Create(WorkFlowsForCreation workFlowsForCreation, Guid userId)
    {
        var newWorkFlow = new WorkFlow
        {
            Name = workFlowsForCreation.Name,
            Version = 1,
        };
        newWorkFlow.UpdateCreationProperties(DateTimeOffset.Now, userId); // Assuming a new Guid for CreatedBy, to update later with actual user ID

        return newWorkFlow;
    }

    // Add Props Marker -- Deleting this comment will cause the add props utility to be incomplete

    public WorkFlow AddStep(Step step, int stepAssignmentIndex)
    {
        var stepAssignment = StepsAssignment.Create(step, stepAssignmentIndex);
        _steps.Add(stepAssignment);
        UpdateSteps(_steps);
        return this;
    }

    public WorkFlow RemoveStep(Step step)
    {
        var stepAssignment = _steps.FirstOrDefault(s => s.StepId == step.Id);
        if (stepAssignment == null)
        {
            return this;
        }
        stepAssignment.UpdateStepCount(stepAssignment.StepIndex - 1);
        _steps.Remove(stepAssignment);
        //UpdateSteps(_steps)
        return this;
    }
    private void UpdateSteps(IList<StepsAssignment> updates)
    {
        // Use HashSet for O(1) lookups. Assumes Step implements proper Equals/GetHashCode (e.g., by Id).
        var currentStepsSet = new HashSet<StepsAssignment>(_steps);
        var updatesSet = new HashSet<StepsAssignment>(updates);

        var additions = updatesSet.Except(currentStepsSet).ToList();
        var removals = currentStepsSet.Except(updatesSet).ToList();

        removals.ForEach(toRemove => _steps.Remove(toRemove));
        additions.ForEach(newStep => _steps.Add(newStep));
    }
    public WorkFlow AddstepAssignment(StepsAssignment stepsAssignment)
    {
        _steps.Add(stepsAssignment);
        return this;
    }
    public WorkFlow RemoveStepAssignment(StepsAssignment stepsAssignment)
    {
        var step = _steps.FirstOrDefault(stepsAssignment);
        if (step is not null)
            _steps.Remove(stepsAssignment);
        return this;
    }
}
