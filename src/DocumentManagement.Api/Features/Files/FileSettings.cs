using System.Globalization;

namespace DocumentManagement.Api.Features.Files;

internal static class FileSettings
{
    public const int MaxFileSizeInMB = 3;
    public const int MaxFileSizeInBytes = MaxFileSizeInMB * 1024 * 1024;
    public static readonly string[] BlockedSignatures = ["4D-5A", "2F-2A", "D0-CF"];
    public static readonly string[] AllowedImagesExtensions = [".jpg", ".jpeg", ".png"];
    public static readonly string[] AllowedDocumentsExtensions = [".pdf", ".docx", ".xlsx"];

    public static readonly string[] AllowedExtensions = AllowedImagesExtensions.Concat(AllowedDocumentsExtensions).ToArray();
    public static readonly string[] AllowedMimeTypes = ["image/jpeg", "image/png", "application/pdf", "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"];
    // PDF file signature (magic number) is "25-50-44-46" (hex for "%PDF")
    public static readonly string PdfSignature = "25-50-44-46";

    public static bool IsSignatureBlocked(byte[] fileHeader)
    {
        foreach (var signature in BlockedSignatures)
        {
            var sigBytes = signature.Split('-')
                .Select(s => byte.Parse(s, NumberStyles.HexNumber, CultureInfo.InvariantCulture))
                .ToArray();

            if (fileHeader.Length >= sigBytes.Length)
            {
                bool match = true;
                for (int i = 0; i < sigBytes.Length; i++)
                {
                    if (fileHeader[i] != sigBytes[i])
                    {
                        match = false;
                        break;
                    }
                }
                if (match)
                    return true;
            }
        }
        return false;
    }
    public static byte[] GetFileHeader(string filePath, int headerLength = 8)
    {
        using var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
        var buffer = new byte[headerLength];
        int bytesRead = fs.Read(buffer, 0, headerLength);
        if (bytesRead < headerLength)
        {
            Array.Resize(ref buffer, bytesRead);
        }
        return buffer;
    }

    public static byte[] GetFileHeader(Stream stream, int headerLength = 8)
    {
        var buffer = new byte[headerLength];
        int bytesRead = stream.Read(buffer, 0, headerLength);
        if (bytesRead < headerLength)
        {
            Array.Resize(ref buffer, bytesRead);
        }
        return buffer;
    }

}

