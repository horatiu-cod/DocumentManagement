using Ardalis.SmartEnum;

namespace DocumentManagement.Domain.StepNames;

public class StepName : ValueObject
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
    public StepName(string value)
    {
        Value = value;
    }
    public static StepName Of(string value) => new(value);
    public static implicit operator string(StepName value) => value.Value;
    public static List<string> ListNames => StepNameEnum.List.Select(x => x.Name).ToList();

    public static StepName AtOwner() => new(StepNameEnum.AtOwner.Name);
    public static StepName AtMonitoring() => new("");
    public static StepName AtAccounting() => new("");
    public static StepName AtLegal() => new("");
    public static StepName AtPurchasing() => new("");
    public static StepName AtContracting() => new("");
    public static StepName AtManager() => new("");
    public static StepName AtGeneralManager() => new("");

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
        public override bool CanStepToNext(StepNameEnum next) => next == AtManager;
    }

    private sealed class AtManagerName : StepNameEnum
    {
        public AtManagerName() : base("Manager", 7) { }
        public override bool CanStepToNext(StepNameEnum next) => next == AtGeneralManager;
    }

    private sealed class AtContractingName : StepNameEnum
    {
        public AtContractingName() : base("Contracting", 3) { }
        public override bool CanStepToNext(StepNameEnum next) => next == AtManager;
    }

    private sealed class AtGeneralManagerName : StepNameEnum
    {
        public AtGeneralManagerName() : base("GeneralManager", 8) { }
        public override bool CanStepToNext(StepNameEnum next) => next == AtOwner;
    }

    private class AtOwnerName : StepNameEnum
    {
        public AtOwnerName() : base("Owner", 0){}
        public override bool CanStepToNext(StepNameEnum next) => next == AtMonitoring;
   }
}
