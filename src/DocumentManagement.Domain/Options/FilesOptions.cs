namespace DocumentManagement.Domain.Options;

public sealed class FilesOptions
{
    public static readonly string Files = "Files";


    public int MaxFileSizeInMB { get; set; }
    // PDF file signature (magic number) is "25-50-44-46" (hex for "%PDF")
    public string[] AllowedSignatures { get; set; } = [];
    //BlockedSignatures = ["4D-5A", "2F-2A", "D0-CF"]
    public string[] BlockedSignatures { get; set; } = [];
    //AllowedDocumentsExtensions = [".pdf", ".docx", ".xlsx"]
    public string[] AllowedFileExtensions { get; set; } = [];
    //AllowedContentTypes = ["application/pdf", "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"]
    public string[] AllowedContentTypes { get; set; } = [];
}
