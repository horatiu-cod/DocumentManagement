using Ardalis.SmartEnum;

namespace DocumentManagement.Domain.WorkFlowStatuses;

public class WorkFlowStatus : ValueObject
{
    private WorkFlowStatusEnum? _status;
    public string Value
    {
        get => _status?.Name ?? string.Empty;
        private set
        {
            if (!WorkFlowStatusEnum.TryFromName(value, true, out var parsed))
                throw new Exception(nameof(Value)); // TODO implement exception
            _status = parsed;
        }
    }
    public WorkFlowStatus(string value)
    {
        Value = value;
    }

    public bool IsActive() => Value == Active().Value;
    public static WorkFlowStatus Of(string value) => new(value);
    public static implicit operator string(WorkFlowStatus value) => value.Value;
    public static List<string> ListNames() => WorkFlowStatusEnum.List.Select(x => x.Name).ToList();
    public static WorkFlowStatus Inactive() => new(WorkFlowStatusEnum.Inactive.Name);
    public static WorkFlowStatus Completed() => new(WorkFlowStatusEnum.Completed.Name);
    public static WorkFlowStatus Active() => new(WorkFlowStatusEnum.Active.Name);
}
internal abstract class WorkFlowStatusEnum : SmartEnum<WorkFlowStatusEnum>
{
    public static readonly WorkFlowStatusEnum Inactive = new InactiveWorkFlowStatus();
    public static readonly WorkFlowStatusEnum Active = new ActiveWorkFlowStatus();
    public static readonly WorkFlowStatusEnum Completed = new CompletedWorkFlowStatus();
    private WorkFlowStatusEnum(string name, int value) : base(name, value)
    {
    }

    public sealed class InactiveWorkFlowStatus : WorkFlowStatusEnum
    {
        public InactiveWorkFlowStatus() : base("Inactive", 0) { }
    }

    private sealed class ActiveWorkFlowStatus : WorkFlowStatusEnum
    {
        public ActiveWorkFlowStatus() : base("Active", 1) { }
    }

    public sealed class CompletedWorkFlowStatus : WorkFlowStatusEnum
    {
        public CompletedWorkFlowStatus() : base("Completed", 2) { }
    }
}
