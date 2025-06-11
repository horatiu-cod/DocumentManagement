using DocumentManagement.Domain.Steps;
using DocumentManagement.Domain.WorkFlows.Models;
using DocumentManagement.Domain.WorkFlowStatuses;

namespace DocumentManagement.Domain.WorkFlows;

internal class WorkFlow : BaseEntity
{
    public string Name { get; private set; } = string.Empty;
    public int Version { get; private set; }
    public WorkFlowStatus Status { get; private set; } = WorkFlowStatus.Started;

    private readonly List<StepsAssignment> _steps  = [];
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

    public WorkFlow AddStep(Step step)
    {
        int stepAssignmentCount = 1;
        if (_steps.Any())
        {
            stepAssignmentCount = _steps.Count() + 1;
        }

        var stepAssignment = StepsAssignment.Create(step, stepAssignmentCount);
        _steps.Add(stepAssignment);
        //UpdateSteps(_steps);
        return this;
    }

    public WorkFlow AddSteps(IList<Step> steps, int stepAssignmentCount)
    {
        foreach (var step in steps)
        {
            var stepAssignment = StepsAssignment.Create(step, stepAssignmentCount);
            _steps.Add(stepAssignment);
        }
        //UpdateSteps(_steps);
        return this;
    }

    public WorkFlow RemoveStep(Step step)
    {
        var stepAssignment = _steps.FirstOrDefault(s => s.StepId == step.Id);
        if (stepAssignment == null)
        {
            return this;
        }
        if (stepAssignment.StepCount > 1)
        {
            stepAssignment.UpdateStepCount(stepAssignment.StepCount - 1);
            return this;
        }
        stepAssignment.UpdateStepCount(stepAssignment.StepCount - 1);
        _steps.Remove(stepAssignment);
        UpdateSteps(_steps);
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
}
