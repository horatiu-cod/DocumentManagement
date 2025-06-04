using DocumentManagement.Domain.Abstractions;
using Microsoft.AspNetCore.Components.Forms;

namespace DocumentManagement.UI.Files;

public class BrowserFileProxy(IBrowserFile browserFile) : IFileProxy
{
    private readonly IBrowserFile _browserFile = browserFile;

    public string FileName => _browserFile.Name;

    public string ContentType => _browserFile.ContentType;

    public string FileExtension => Path.GetExtension(_browserFile.Name);

    public long Length => _browserFile.Size;

    public async Task CopyToAsync(Stream target, CancellationToken cancellationToken = default)
    {
        using var s = _browserFile.OpenReadStream(_browserFile.Size, cancellationToken);

        await s.CopyToAsync(target, cancellationToken);
    }

    public async Task<byte[]> GetData(CancellationToken cancellationToken = default)
    {
        using var memoryStream = new MemoryStream();
        await  _browserFile.OpenReadStream(_browserFile.Size, cancellationToken).CopyToAsync(memoryStream, cancellationToken);
        return memoryStream.ToArray();
    }
}
