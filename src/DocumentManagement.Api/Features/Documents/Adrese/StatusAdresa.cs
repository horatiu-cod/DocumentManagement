using Ardalis.SmartEnum;

namespace DocumentManagement.Api.Features.Documents.Adrese;

public abstract class StatusAdresa : SmartEnum<StatusAdresa>
{
    public static readonly StatusAdresa Creat = new CreatStatus();

    public static readonly StatusAdresa TransmisVerificare = new TransmisVerificareStatus();

    protected StatusAdresa(string name, int value) : base(name, value)
    {
    }

    public abstract bool IsEditable { get; }

    public abstract bool CanStepTo(StatusAdresa next);

    private sealed class CreatStatus : StatusAdresa
    {
        public CreatStatus() : base("Creat", 0)
        {
        }
        public override bool CanStepTo(StatusAdresa next) => next == StatusAdresa.TransmisVerificare || next == StatusAdresa.Creat;

        public override bool IsEditable => throw new NotImplementedException();

    }

    private sealed class TransmisVerificareStatus : StatusAdresa
    {
        public TransmisVerificareStatus() : base("TransmisVerificare", 1)
        {
        }

        public override bool IsEditable => throw new NotImplementedException();

        public override bool CanStepTo(StatusAdresa next)
        {
            throw new NotImplementedException();
        }
    }

}
/*
    Creat,
    TransmisVerificare,
    InVerificare,
    Verificat,
    TransmisAvizare,
    InAvizare,
    Avizat,
    TransmisAprobare,
    InAprobare,
    Aprobat,
    TransmisOwner,
    Respins
*/