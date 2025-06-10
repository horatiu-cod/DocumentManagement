using Ardalis.SmartEnum;

namespace DocumentManagement.Domain.WorkFlowStatuses;

internal abstract class WorkFlowStatus : SmartEnum<WorkFlowStatus>
{
    public static readonly WorkFlowStatus InActive = new InActiveWorkFlowStatus();
    public static readonly WorkFlowStatus Started = new StartedWorkFlowStatus();
    public static readonly WorkFlowStatus Completed = new CompletedWorkFlowStatus();
    private WorkFlowStatus(string name, int value) : base(name, value)
    {
    }

    public sealed class InActiveWorkFlowStatus : WorkFlowStatus
    {
        public InActiveWorkFlowStatus() : base("Allocated", 0)
        {
        }
    }

    private sealed class StartedWorkFlowStatus : WorkFlowStatus
    {
        public StartedWorkFlowStatus() : base("Upload", 1)
        {
        }
    }

    public sealed class CompletedWorkFlowStatus : WorkFlowStatus
    {
        public CompletedWorkFlowStatus() : base("Completed", 2)
        {
        }
    }
}
