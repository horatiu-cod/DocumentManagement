namespace DocumentManagement.Api.Features.Documents.Settings;

internal static class FileSettings
{
    public const int MaxFileSizeInMB = 3;
    public const int MaxFileSizeInBytes = MaxFileSizeInMB * 1024 * 1024;
    public static readonly string[] BlockedSignatures = ["4D-5A", "2F-2A", "D0-CF"];
    public static readonly string[] AllowedImagesExtensions = [".jpg", ".jpeg", ".png"];
    public static readonly string[] AllowedDocumentsExtensions = [".pdf", ".docx", ".xlsx"];

    public static readonly string[] AllowedExtensions = AllowedImagesExtensions.Concat(AllowedDocumentsExtensions).ToArray();
    public static readonly string[] AllowedMimeTypes = ["image/jpeg", "image/png", "application/pdf", "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"];
}

