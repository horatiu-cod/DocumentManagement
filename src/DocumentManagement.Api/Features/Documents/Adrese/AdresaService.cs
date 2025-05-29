using System;

namespace DocumentManagement.Api.Features.Documents.Adrese;

public sealed class AdresaService(ILogger<AdresaService> logger) : IAdresaService
{
    private readonly ILogger<AdresaService> _logger = logger;
    //Simuleaza o baza de date sau un repository
    private List<Adresa> _adrese = [];

    // Metode Owner
    public Adresa CreazaAdresa(Guid ownerId)
    {
        var adresa = new Adresa { OwnerId = ownerId };
        _adrese.Add(adresa);

        _logger.LogInformation("Adresa {Adresa.Id} a fost creata de owner-ul {OwnerId}", adresa.Id, ownerId);

        return adresa;
    }

    public bool SemneazaAdresaOwner(Guid adresaId, Guid ownerId)
    {
        var adresa = _adrese.FirstOrDefault(a => a.Id == adresaId && a.OwnerId == ownerId);

        if (adresa == null)
        {
            _logger.LogError("Erroare: Adresa {AdresaId} nu este gasita sau nu apartine owner-ului {OwnerId}", adresaId, ownerId);

            return false;
        }

        if (adresa.Status != StatusAdresa.Creat || adresa.Status != StatusAdresa.Aprobat || !adresa.Editable)
        {
            _logger.LogError("Erroare: Adresa {AdresaId} nu poate fi semnata in starea curenta {Adresa.Status} sau nu este in editare", adresaId, adresa.Status);

            return false;
        }

        adresa.SemnatOwner = true;

        adresa.Editable = false;// editare blocata

        _logger.LogInformation("Adresa {AdresaId} a fost semnata de owner {OwnerId}. Editare blocata.", adresaId, ownerId);
        return true;
    }
}
