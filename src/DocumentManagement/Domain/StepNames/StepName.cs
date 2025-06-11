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


    private StepNameEnum(string name, int value) : base(name, value)
    {
    }

    public class AtMonitoringName : StepNameEnum
    {
    }

    public class AtAccountingName : StepNameEnum
    {
        public 
    }

    public class AtLegalName : StepNameEnum
    {
    }

    private class AtPurchasingName : StepNameEnum
    {
        public AtPurchasingName() : base("Purchasing", 4){}
    }

    private sealed class AtManagerName : StepNameEnum
    {
        public AtManagerName() : base("Manager", 7){}
    }

    private sealed class AtContractingName : StepNameEnum
    {
        public AtContractingName() : base("Contracting", 3){}
    }

    private sealed class AtGeneralManagerName : StepNameEnum
    {
       public AtGeneralManagerName() : base("GeneralManager", 8){}
    }

    private class AtOwnerName : StepNameEnum
    {
        public AtOwnerName() : base("Owner", 0)
        {
        }
    }
}
