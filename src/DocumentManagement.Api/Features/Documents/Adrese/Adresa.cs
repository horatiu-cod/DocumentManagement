using DocumentManagement.Domain.Entities;

namespace DocumentManagement.Api.Features.Documents.Adrese;

public sealed class Adresa : BaseEntity
{
    public bool Editable { get; set; }
    public Guid OwnerId => CreatedBy; // Assuming CreatedBy is the owner of the address
    public DateTimeOffset CreatedAt { get; set; }
    public bool SemnatOwner { get; set; }
    public DateTimeOffset? AlocatOwnerAt { get; set; }
    public DateTimeOffset? TransmisVerificareAt { get; set; }
    public DateTimeOffset? AlocatVerificareAt { get; set; }
    public Guid VerificatorId { get; set; }
    public bool SemnatVerificare { get; set; }
    public DateTimeOffset? TransmisAvizareAt { get; set; }
    public DateTimeOffset? AlocatAvizareAt { get; set; }
    public Guid AvizorId { get; set; }
    public bool SemnatAvizare { get; set; }
    public DateTimeOffset? TransmisAprobareAt { get; set; }
    public DateTimeOffset? AlocatAprobareAt { get; set; }
    public Guid AprobatorId { get; set; }
    public bool SemnatAprobare { get; set; }
    public DateTimeOffset? TransmisOwnerAt { get; set; }
    public string Nota { get; set; } = string.Empty;
    public StatusAdresa Status { get; private set; }

    public Adresa()
    {
        Editable = StatusAdresa.Creat.IsEditable;
        CreatedAt = DateTime.Now;
        Status = StatusAdresa.Creat;
        SemnatOwner = false;
        SemnatVerificare = false;
        SemnatAvizare = false;
        SemnatAprobare = false;
    }

}
