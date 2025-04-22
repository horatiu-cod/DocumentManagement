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

app.MapGet("/listall", () => 
{
   string domain = Environment.UserDomainName;
string userName = Environment.UserName;
string currentUser = $"{domain}\\{userName}";
Console.WriteLine("Current User: " + currentUser);


     Console.WriteLine("\r\nExists Certs Name and Location");
        Console.WriteLine("------ ----- -------------------------");

        foreach (StoreLocation storeLocation in (StoreLocation[])
            Enum.GetValues(typeof(StoreLocation)))
        {
            foreach (StoreName storeName in (StoreName[])
                Enum.GetValues(typeof(StoreName)))
            {
                X509Store store = new X509Store(storeName, storeLocation);

                try
                {
                    store.Open(OpenFlags.OpenExistingOnly);

                    Console.WriteLine("Yes    {0,4}  {1}, {2}",
                        store.Certificates.Count, store.Name, store.Location);
                }
                catch (CryptographicException)
                {
                    Console.WriteLine("No           {0}, {1}",
                        store.Name, store.Location);
                }
            }
            Console.WriteLine();
        }
    //store.Close();
    return Results.Ok();
});

app.MapGet("/list", () => 
{
    X509Store store = new X509Store( StoreName.My, StoreLocation.CurrentUser);

    store.Open(OpenFlags.ReadOnly | OpenFlags.OpenExistingOnly);

    X509Certificate2Collection collection = store.Certificates;
    X509Certificate2Collection fcollection = collection.Find(X509FindType.FindByTimeValid, DateTime.Now, false);
    //X509Certificate2Collection scollection = X509Certificate2UI.SelectFromCollection(fcollection, "Test Certificate Select", "Select a certificate from the following list to get information on that certificate", X509SelectionFlag.MultiSelection);

    foreach (X509Certificate2 x509 in fcollection)
    {
        var rawdata = x509.RawData;
        Console.WriteLine($"Subject: {x509.Subject}");
        Console.WriteLine($"Issuer: {x509.Issuer}");
        Console.WriteLine($"Valid From: {x509.NotBefore}");
        Console.WriteLine($"Valid To: {x509.NotAfter}");
        Console.WriteLine($"Thumbprint: {x509.Thumbprint}");
        Console.WriteLine();
        Console.WriteLine("Content Type: {0}{1}", X509Certificate2.GetCertContentType(rawdata), Environment.NewLine);
        Console.WriteLine("Friendly Name: {0}{1}", x509.FriendlyName, Environment.NewLine);
        Console.WriteLine("Certificate Verified?: {0}{1}", x509.Verify(), Environment.NewLine);
        Console.WriteLine("Simple Name: {0}{1}", x509.GetNameInfo(X509NameType.SimpleName, true), Environment.NewLine);
        Console.WriteLine("Signature Algorithm: {0}{1}", x509.SignatureAlgorithm.FriendlyName, Environment.NewLine);
        Console.WriteLine("Certificate Archived?: {0}{1}", x509.Archived, Environment.NewLine);
        Console.WriteLine("Length of Raw Data: {0}{1}", x509.RawData.Length, Environment.NewLine);
        //X509Certificate2UI.DisplayCertificate(x509);
        //x509.Reset();
    }
    store.Close();
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

