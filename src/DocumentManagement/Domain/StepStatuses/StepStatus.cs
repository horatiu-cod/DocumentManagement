using Ardalis.SmartEnum;

namespace DocumentManagement.Domain.StepStatuses;

public class StepStatus : ValueObject
{
    private StepNameEnum? _status;

    public string Value
    {
        get => _status?.Name ?? string.Empty;
        private set
        {
            if (!StepNameEnum.TryFromName(value, true, out var parsed))
                throw new Exception(nameof(Value));// TODO implement exception 

            _status = parsed;
        }
    }
    public StepStatus(string value)
    {
        Value = value;
    }
    public static StepStatus Of(string value) => new(value);
    public static implicit operator string(StepStatus value) => value.Value;
    public static List<string> ListNames() => StepNameEnum.List.Select(x => x.Name).ToList();

    public static StepStatus AtOwner() => new(StepNameEnum.AtOwner.Name);
    public static StepStatus AtMonitoring() => new(StepNameEnum.AtMonitoring.Name);
    public static StepStatus AtAccounting() => new(StepNameEnum.AtAccounting.Name);
    public static StepStatus AtLegal() => new(StepNameEnum.AtLegal.Name);
    public static StepStatus AtPurchasing() => new(StepNameEnum.AtPurchasing.Name);
    public static StepStatus AtContracting() => new(StepNameEnum.AtContracting.Name);
    public static StepStatus AtManager() => new(StepNameEnum.AtManager.Name);
    public static StepStatus AtGeneralManager() => new(StepNameEnum.AtGeneralManager.Name);

}
public abstract class StepNameEnum : SmartEnum<StepNameEnum>
{
    public static readonly StepNameEnum AtOwner = new AtOwnerName();
    public static readonly StepNameEnum AtMonitoring = new AtMonitoringName();
    public static readonly StepNameEnum AtAccounting = new AtAccountingName();
    public static readonly StepNameEnum AtLegal = new AtLegalName();
    public static readonly StepNameEnum AtPurchasing = new AtPurchasingName();
    public static readonly StepNameEnum AtManager = new AtManagerName();
    public static readonly StepNameEnum AtContracting = new AtContractingName();
    public static readonly StepNameEnum AtGeneralManager = new AtGeneralManagerName();

    public abstract bool CanStepToNext(StepNameEnum next);
  

    private StepNameEnum(string name, int value) : base(name, value)
    {
    }

    private sealed class AtMonitoringName : StepNameEnum
    {
        public AtMonitoringName() : base("Monitoring", 1){}

        public override bool CanStepToNext(StepNameEnum next) => next == AtAccounting || next == AtOwner || next == AtContracting || next == AtLegal || next == AtGeneralManager || next == AtPurchasing || next == AtManager;
    }

    private sealed class AtAccountingName : StepNameEnum
    {
        public AtAccountingName() : base("Accounting", 2) { }
        public override bool CanStepToNext(StepNameEnum next) => next == AtContracting;
    }

    private sealed class AtLegalName : StepNameEnum
    {
        public AtLegalName() : base("Legal", 5) { }
        public override bool CanStepToNext(StepNameEnum next) => next == AtManager;

    }

    private sealed class AtPurchasingName : StepNameEnum
    {
        public AtPurchasingName() : base("Purchasing", 4) { }
        public override bool CanStepToNext(StepNameEnum next) => next == AtLegal;
    }

    private sealed class AtManagerName : StepNameEnum
    {
        public AtManagerName() : base("Manager", 7) { }
        public override bool CanStepToNext(StepNameEnum next) => next == AtGeneralManager;
    }

    private sealed class AtContractingName : StepNameEnum
    {
        public AtContractingName() : base("Contracting", 3) { }
        public override bool CanStepToNext(StepNameEnum next) => next == AtLegal;
    }

    private sealed class AtGeneralManagerName : StepNameEnum
    {
        public AtGeneralManagerName() : base("GeneralManager", 8) { }
        public override bool CanStepToNext(StepNameEnum next) => next == AtOwner;
    }

    private sealed class AtOwnerName : StepNameEnum
    {
        public AtOwnerName() : base("Owner", 0){}
        public override bool CanStepToNext(StepNameEnum next) => next == AtMonitoring;
   }
}
