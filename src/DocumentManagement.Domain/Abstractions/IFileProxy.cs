namespace DocumentManagement.Domain.Abstractions;

public interface IFileProxy
{
    string FileName { get; }
    string ContentType { get; }
    string FileExtension { get; }
    long Length { get; }
    Task CopyToAsync(Stream target, CancellationToken cancellationToken = default);
    Task<byte[]> GetData(CancellationToken cancellationToken = default);
}
