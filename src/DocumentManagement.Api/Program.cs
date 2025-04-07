using System.Security.AccessControl;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Define the directory for file storage
var uploadDirectory = Path.Combine(Directory.GetCurrentDirectory(), "uploads");
if (!Directory.Exists(uploadDirectory))
{
    Directory.CreateDirectory(uploadDirectory);
}

// Retrieve encryption key from configuration (must be 32 bytes for AES-256)
var encryptionKey = Encoding.UTF8.GetBytes(config["EncryptionKey"] ?? "12345678901234567890123456789012");

static async Task EncryptAndSaveFileAsync(IFormFile file, string filePath, byte[] key)
{
    using var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None);
    using var aes = Aes.Create();
    aes.Key = key;
    aes.GenerateIV();
    await fileStream.WriteAsync(aes.IV.AsMemory(0, aes.IV.Length));
    using var cryptoStream = new CryptoStream(fileStream, aes.CreateEncryptor(), CryptoStreamMode.Write);
    await file.CopyToAsync(cryptoStream);
}

static async Task<FileStream> DecryptFileAsync(string filePath, byte[] key)
{
    var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
    var iv = new byte[16];
    await fileStream.ReadAsync(iv, 0, iv.Length);
    var aes = Aes.Create();
    aes.Key = key;
    aes.IV = iv;
    var cryptoStream = new CryptoStream(fileStream, aes.CreateDecryptor(), CryptoStreamMode.Read);
    return new CryptoStreamWrapper(cryptoStream, fileStream);
}

// Upload file endpoint with encryption
app.MapPost("/upload", async (IFormFile file) =>
{
    if (file == null || file.Length == 0)
    {
        return Results.BadRequest("No file uploaded.");
    }

    var newFileName = $"{Guid.NewGuid()}_{file.FileName}";
    var filePath = Path.Combine(uploadDirectory, newFileName);
    await EncryptAndSaveFileAsync(file, filePath, encryptionKey);

    return Results.Ok($"File uploaded successfully as {newFileName}.");
});

// Download file endpoint with decryption
app.MapGet("/download/{fileName}", async (string fileName) =>
{
    var filePath = Path.Combine(uploadDirectory, fileName);
    if (!System.IO.File.Exists(filePath))
    {
        return Results.NotFound("File not found.");
    }

    var decryptedStream = await DecryptFileAsync(filePath, encryptionKey);
    return Results.File(decryptedStream, "application/octet-stream", fileName);
});

app.MapGet("/list", () => 
{
    X509Store store = new X509Store("MY", StoreLocation.CurrentUser);

    store.Open(OpenFlags.ReadOnly);
    X509Certificate2Collection col = store.Certificates;

    var localMachineStore = new X509Store("MY", StoreLocation.LocalMachine);
    localMachineStore.Open(OpenFlags.ReadOnly);
    var certificates = localMachineStore.Certificates;
    localMachineStore.Close();
    return Results.Ok();
});

app.Run();


// Helper class to ensure streams are disposed properly
class CryptoStreamWrapper(CryptoStream cryptoStream, FileStream fileStream) : FileStream(fileStream.SafeFileHandle, FileAccess.Read)
{
    private readonly CryptoStream _cryptoStream = cryptoStream;

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _cryptoStream.Dispose();
        }
        base.Dispose(disposing);
    }
}

