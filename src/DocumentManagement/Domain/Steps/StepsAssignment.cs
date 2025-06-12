namespace DocumentManagement.Domain.Steps;

internal class StepsAssignment : BaseEntity
{
    public Guid StepId { get; private set; }
    public Step? Step { get; private set; }
    public Guid WorkFlowId { get; }
    public int StepIndex { get; private set; }
    public static StepsAssignment Create(Step step, int stepCount)
    {
        var newStepAssignment =  new StepsAssignment
        {
            StepId = step.Id,
            Step = step,
            StepIndex = stepCount,
        };
        return newStepAssignment;
    }
    public void UpdateStepCount(int stepCount)
    {
        StepIndex = stepCount;
    }
}
