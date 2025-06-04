using System;
using DocumentManagement.Domain.Abstractions;
using Microsoft.AspNetCore.Components.Forms;

namespace DocumentManagement.UI.Files;

public class BrowserFileProxy(IBrowserFile browserFile) : IFileProxy
{
    private readonly IBrowserFile _browserFile ?? throw new 
    public string FileName => _browserFile.Name;

    public string ContentType => browserFile.ContentType;

    public string FileExtension => Path.GetExtension(browserFile.Name);

    public long Length => browserFile.Size;

    public Task CopyToAsync(Stream target, CancellationToken cancellationToken = default)
    {
        await 
    }

    public Task<byte[]> GetData(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
