namespace DocumentManagement.Api.Features.Documents.Adrese;

public sealed class Adresa
{
 public Guid Id {get; set;}
 public bool Editable {get; set;}
 public Guid OwnerId {get; set;}
 public DateTimeOffset CreatedAt {get; set;}
 public bool SemnatOwner {get; set;}
 public DateTimeOffset? TransmisVerificareAt {get; set;}
 public DateTimeOffset? AlocatVerificareAt {get; set;}
 public Guid VerificatorId {get; set;}
 public bool SemnatVerificare {get; set;}
 public DateTimeOffset? TransmisAvizareAt {get; set;}
 public DateTimeOffset? AlocatAvizareAt {get; set;}
 public Guid AvizorId {get; set;}
 public bool SemnatAvizare {get; set;}
 public DateTimeOffset? TransmisAprobareAt {get; set;}
 public DateTimeOffset? AlocatAprobareAt {get; set;}
 public Guid AprobatorId {get; set;}
 public bool SemnatAprobare {get; set;}
 public string Nota {get; set;} = string.Empty;
 public StatusAdresa Status {get; set;}

 public Adresa()
 {
    Id = Guid.NewGuid();
    Editable = true;
    CreatedAt = DateTime.Now;
    Status = StatusAdresa.Creat;
    SemnatOwner = false;
    SemnatVerificare = false;
    SemnatAvizare = false;
    SemnatAprobare = false;
 }
 
}

public enum StatusAdresa
{
    Creat,
    TransmisVerificare,
    IVerificare,
    Verificat,
    TransmisAvizare,
    InAvizare,
    Avizat,
    TransmisAprobare,
    InAprobare,
    Aprobat,
    TransmisOwner,
    Respins
}