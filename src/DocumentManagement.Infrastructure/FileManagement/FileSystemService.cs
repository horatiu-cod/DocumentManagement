using Microsoft.Extensions.Logging;
using FluentResults;
﻿using Ardalis.GuardClauses;
using System.Text;

namespace DocumentManagement.Infrastructure.FileManagement;
internal class FileSystemService
{
    readonly ILogger<FileSystemService> _logger;
    private readonly string _systemRootMain;

    public FileSystemService(ILogger<FileSystemService> logger, string SystemRootMain)
    {
        _logger = logger;
        _systemRootMain = Guard.Against.NullOrEmpty(SystemRootMain);
        Init();
    }

    private void Init ()
    {
        try
        {
            if (!Directory.Exists(_systemRootMain))
            {
                Directory.CreateDirectory(_systemRootMain);
            }
        }
        catch ( Exception e)
        {
            _logger.LogError(e, "{Message}, {StackTrace}", e.Message, e.StackTrace);
            throw new InvalidOperationException("The system root directory could not be created or accessed. Please check the path and permissions.", e);
        }
    }
     // Initialize the file store by generating a random folder structure
    public static string InitializeFileStore(int maxDepth = 6)
    {
        var fileStore = GenerateRandomStructure(maxDepth);
        return fileStore;
    }

    // Generate a random folder structure with a specified maximum depth
    private static string GenerateRandomStructure(int maxDepth)
    {
        var depth = new Random().Next(2, maxDepth);
        var finalStructure = new StringBuilder();
        for (int i = 0; i < depth; i++)
        {
            finalStructure.Append($"\\{Guid.NewGuid().ToString()[..8]}");
        }
        return finalStructure.ToString();
    }

    // Write a file to the file store
    public  Result<string> WriteFileToStore(MemoryStream memoryStream, string fileStore, string fileName)
    {
        try
        {
            // Convert string to Base64
            var filePath = Convert.ToBase64String(Encoding.UTF8.GetBytes(fileName)) + ".adr";
            var path = Path.Combine(_systemRootMain + fileStore);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            
            var fullPath = Path.Combine(path, filePath);
            using FileStream fileStream = new(fullPath, FileMode.Create, FileAccess.Write);

                memoryStream.WriteTo(fileStream);
                fileStream.Close();

            return Result.Ok(Path.Combine(path, fileName)).ToResult<string>();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "{Message}, {StackTrace}" , e.Message, e.StackTrace);
            return Result.Fail ("An error occurred while trying to write a file to the file store.").ToResult<string>();
        }
    }

    // Read a file from the file store
    public Result<MemoryStream> ReadFileFromStore(string fileStore, string filePath)
    {
        try
        {
            var fullPath = Path.Combine(_systemRootMain, fileStore, filePath);
            if (!File.Exists(fullPath))
            {
                return Result.Fail("File not found in the store.").ToResult<MemoryStream>();
            }
            var memoryStream = new MemoryStream();
            using (var fileStream = new FileStream(fullPath, FileMode.Open, FileAccess.Read))
            {
                fileStream.CopyTo(memoryStream);
            }
            memoryStream.Position = 0; // Reset stream position to the beginning
            return Result.Ok(memoryStream).ToResult<MemoryStream>();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "{Message}, {StackTrace}", e.Message, e.StackTrace);
            return Result.Fail("An error occurred while trying to read a file from the file store.").ToResult<MemoryStream>();
        }
    }
}
