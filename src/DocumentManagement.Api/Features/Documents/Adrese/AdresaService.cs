

namespace DocumentManagement.Api.Features.Documents.Adrese;

public sealed class AdresaService : IAdresaService
{
    private readonly ILogger<DocumentsController> _logger;
    //Simuleaza o baza de date sau un repository
    private List<Adresa> _adrese;

    public AdresaService(ILogger<DocumentsController> logger)
    {
        _logger = logger;
        _adrese = new List<Adresa>();
    }

    // Metode Owner
    public async Task<Adresa> CreazaAdresaAsync(Giud ownerId)
    {
        var adresa = new Adresa (OwnerId = ownerId);
        await _adrese.AddAsync(adresa);

        _logger.LogInformation($"Adresa {adresa.Id} a fost creata de owner-ul {ownerId}")
    }

    public async Task<bool> SemneazaAdresaOwnerAsync(Giud adresaId, Giud ownerId)
    {
        var adresa = _adrese.FirstOrDefault(a => a.Id == adresaId && a.OwnerId == ownerId);

        if (adresa == null)
        {
            _logger.LogError($"Erroare: Adresa {adresaId} nu este gasita sau nu apartine owner-ului {ownerId}");
            return false;
        }

        if (adresa.Status != AdresaStatus.Creat)

    }

}