using System.Security.Cryptography;
using System.Text;

namespace DocumentManagement.Api.Features.Files;

public class FileService
{
    private readonly string _uploadDirectory;
    private readonly byte[] _encryptionKey;
    private readonly string[] _allowedExtensions = { ".txt", ".pdf", ".jpg", ".png", ".docx" };

    public FileService(IConfiguration config) // Inject IConfiguration
    {
        _uploadDirectory = Path.Combine(Directory.GetCurrentDirectory(), "uploads");
        if (!Directory.Exists(_uploadDirectory))
        {
            Directory.CreateDirectory(_uploadDirectory);
        }

        _encryptionKey = Encoding.UTF8.GetBytes(config["EncryptionKey"] ?? "12345678901234567890123456789012");

        if (_encryptionKey.Length != 32)
        {
            throw new ArgumentException("EncryptionKey must be 32 bytes (256 bits).");
        }
    }

    public async Task<IResult> UploadFileAsync(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            return Results.BadRequest("No file uploaded.");
        }

        var fileExtension = Path.GetExtension(file.FileName).ToLower();
        if (!_allowedExtensions.Contains(fileExtension))
        {
            return Results.BadRequest("File type not allowed.");
        }

        var newFileName = $"{Guid.NewGuid()}_{file.FileName}";
        var filePath = Path.Combine(_uploadDirectory, newFileName);
        await EncryptAndSaveFileAsync(file, filePath);

        return Results.Ok($"File uploaded successfully as {newFileName}.");
    }

    public async Task<IResult> DownloadEncryptedFileAsync(string fileName, CancellationToken cancellationToken)
    {
        var filePath = Path.Combine(_uploadDirectory, fileName);
        if (!File.Exists(filePath))
        {
            return Results.NotFound("File not found.");
        }

        var decryptedStream = await DecryptFileAsync(filePath, cancellationToken);
        return Results.File(decryptedStream, "application/octet-stream", fileName);
    }

    private async Task EncryptAndSaveFileAsync(IFormFile file, string filePath)
    {
        using var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None);
        using var aes = Aes.Create();
        aes.Key = _encryptionKey;
        aes.GenerateIV();
        await fileStream.WriteAsync(aes.IV.AsMemory(0, aes.IV.Length));
        using var cryptoStream = new CryptoStream(fileStream, aes.CreateEncryptor(), CryptoStreamMode.Write);
        await file.CopyToAsync(cryptoStream);
    }

    private async Task<FileStream> DecryptFileAsync(string filePath, CancellationToken cancellationToken)
    {
        using var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
        var iv = new byte[16];
        await fileStream.ReadAsync(iv.AsMemory( 0, iv.Length), cancellationToken);
        using var aes = Aes.Create();
        aes.Key = _encryptionKey;
        aes.IV = iv;
        var cryptoStream = new CryptoStream(fileStream, aes.CreateDecryptor(aes.Key, aes.Key), CryptoStreamMode.Read);
        return new CryptoStreamWrapper(cryptoStream, fileStream);
    }
}

// Helper class to ensure streams are disposed properly
class CryptoStreamWrapper : FileStream
{
    private readonly CryptoStream _cryptoStream;
    public CryptoStreamWrapper(CryptoStream cryptoStream, FileStream fileStream)
        : base( fileStream.Name, FileMode.Open, FileAccess.Read)
    {
        _cryptoStream = cryptoStream;
    }
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _cryptoStream.Dispose();
        }
        base.Dispose(disposing);
    }
}
