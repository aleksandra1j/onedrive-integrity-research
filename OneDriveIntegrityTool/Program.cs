using OneDriveIntegrityTool.Services;

namespace OneDriveIntegrityTool
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("OneDrive File Integrity Research Tool");
            Console.WriteLine("=====================================");

            try
            {
                Console.WriteLine("Setting up mock OneDrive service for testing...");
                Console.WriteLine("Note: Using local mock due to Microsoft authentication issues\n");

                var oneDriveService = new MockOneDriveService();
                var folderId = await oneDriveService.CreateTestFolderAsync();
                Console.WriteLine($"Test folder ready! ID: {folderId}\n");

                string[] testFiles = Directory.GetFiles("TestFiles");
                Console.WriteLine($"Found {testFiles.Length} test files to process\n");

                foreach (string testFilePath in testFiles)
                {
                    if (Path.GetFileName(testFilePath).StartsWith("downloaded-"))
                        continue;

                    var fileInfo = new FileInfo(testFilePath);
                    Console.WriteLine($"=== Testing: {Path.GetFileName(testFilePath)} ({fileInfo.Length} bytes) ===");

                    var uploadedFileName = await oneDriveService.UploadFileAsync(testFilePath, folderId);

                    byte[] downloadedContent = await oneDriveService.DownloadFileAsync(uploadedFileName, folderId);
                    string downloadPath = Path.Combine("TestFiles", $"downloaded-{uploadedFileName}");
                    await File.WriteAllBytesAsync(downloadPath, downloadedContent);

                    var integrityService = new FileIntegrityService();
                    bool filesMatch = integrityService.CompareFiles(testFilePath, downloadPath);

                    if (filesMatch)
                    {
                        Console.WriteLine("SUCCESS: File integrity preserved!");
                    }
                    else
                    {
                        Console.WriteLine("FAILURE: File integrity compromised!");
                    }
                    Console.WriteLine();
                }

                Console.WriteLine("All file integrity tests complete!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }

}