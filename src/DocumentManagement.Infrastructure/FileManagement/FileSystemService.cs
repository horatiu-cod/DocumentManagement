using Microsoft.Extensions.Logging;
using FluentResults;
﻿using Ardalis.GuardClauses;
using System.Text;

namespace DocumentManagement.Infrastructure.FileManagement;
internal class FileSystemService
{
    readonly ILogger<FileSystemService> _logger;
    public static string SystemRootMain => "C:\\ProgramData\\Codelabs\\DMS\\FileStores\\Main";

    public FileSystemService(ILogger<FileSystemService> logger)
    {
        Init();
        _logger = logger;
    }

    private static void Init ()
    {
        if (!Directory.Exists(SystemRootMain))
            {
                Directory.CreateDirectory(SystemRootMain);
            }
    }
     // Initialize the file store by generating a random folder structure
    public static Task<string> InitializeFileStore()
    {
        var folderName = GenerateRandomStructure();
        return Task.FromResult(folderName);
    }

    // Generate a random folder structure with a specified maximum depth
    private static string GenerateRandomStructure(int maxDepth = 6)
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
    public async Task<Result<string>> WriteFileToStore(MemoryStream memoryStream, string fileStore)
    {
        try
        {
            var randomFolder = GenerateRandomStructure(4);
            var fileName = Guid.NewGuid().ToString() + ".dms";
            var path = Path.Combine(fileStore, randomFolder);
            var filePath = Path.Combine(SystemRootMain + path);
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
            
            var fullPath = Path.Combine(filePath, fileName);
            FileStream fileStream = new(fullPath, FileMode.Create, FileAccess.Write);

            await Task.Run(() =>
            {
                memoryStream.WriteTo(fileStream);
                fileStream.Close();
            });

            return Result.Ok(Path.Combine(path, fileName)).ToResult<string>();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message + e.StackTrace);
            return Result.Fail ("An error occurred while trying to write a file to the file store.").ToResult<string>();
        }
    }
}
