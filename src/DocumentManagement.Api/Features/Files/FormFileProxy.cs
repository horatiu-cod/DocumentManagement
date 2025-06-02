using Ardalis.GuardClauses;
using DocumentManagement.Domain.Abstractions;

namespace DocumentManagement.Api.Features.Files
{
    public class FormFileProxy(IFormFile formFile) : IFileProxy
    {
        private readonly IFormFile _formFile = Guard.Against.Null(formFile );

        public string FileName => _formFile.FileName;

        public string ContentType => _formFile.ContentType;

        public string FileExtension => Path.GetExtension(_formFile.FileName).ToLowerInvariant();

        public long Length => _formFile.Length;

        public async Task CopyToAsync(Stream target, CancellationToken cancellationToken = default)
        {
            await _formFile.CopyToAsync(target, cancellationToken);
        }

        public async Task<byte[]> GetData(CancellationToken cancellationToken = default)
        {
            using var memoryStream = new MemoryStream();
            await _formFile.CopyToAsync(memoryStream, cancellationToken);
            return memoryStream.ToArray();
        }
    }
}
