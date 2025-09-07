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

                Console.WriteLine("Testing file upload...");
                string testFilePath = Path.Combine("TestFiles", "test-file.txt");

                if (File.Exists(testFilePath))
                {
                    var uploadedFileName = await oneDriveService.UploadFileAsync(testFilePath, folderId);
                    Console.WriteLine($"Upload test complete!\n");

                    Console.WriteLine("Testing file download...");
                    byte[] downloadedContent = await oneDriveService.DownloadFileAsync("test-file.txt", folderId);

                    string downloadPath = Path.Combine("TestFiles", "downloaded-test-file.txt");
                    await File.WriteAllBytesAsync(downloadPath, downloadedContent);
                    Console.WriteLine($"File downloaded and saved to: {downloadPath}\n");

                    Console.WriteLine("Testing file integrity...");
                    var integrityService = new FileIntegrityService();
                    bool filesMatch = integrityService.CompareFiles(testFilePath, downloadPath);

                    if (filesMatch)
                    {
                        Console.WriteLine("SUCCESS: File integrity preserved! Files are identical.");
                    }
                    else
                    {
                        Console.WriteLine("FAILURE: File integrity compromised! Files differ.");
                    }

                }
                else
                {
                    Console.WriteLine($"Test file not found: {testFilePath}");
                    Console.WriteLine("Please create TestFiles/test-file.txt first");
                }

                Console.WriteLine("File upload testing complete!");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }

}