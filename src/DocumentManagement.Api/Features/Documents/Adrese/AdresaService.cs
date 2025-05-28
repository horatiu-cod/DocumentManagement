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

        _logger.LogInformation($"Adresa {adresa.Id} a fost creata de owner-ul {ownerId}");

        return adresa;
    }

    public bool SemneazaAdresaOwnerAsync(Guid adresaId, Guid ownerId)
    {
        var adresa = _adrese.FirstOrDefault(a => a.Id == adresaId && a.OwnerId == ownerId);

        if (adresa == null)
        {
            _logger.LogError($"Erroare: Adresa {adresaId} nu este gasita sau nu apartine owner-ului {ownerId}");

            return false;
        }

        if (adresa.Status != StatusAdresa.Creat)
        {

        }

    }

}

public interface IAdresaService
{
}