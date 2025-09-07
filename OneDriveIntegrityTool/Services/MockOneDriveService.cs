using System.IO;

namespace OneDriveIntegrityTool.Services
{
    public class MockOneDriveService
    {
        private readonly string _mockOneDriveRoot;
        private readonly string _testFolderPath;

        public MockOneDriveService()
        {
            _mockOneDriveRoot = Path.Combine(Directory.GetCurrentDirectory(), "MockOneDrive");
            _testFolderPath = Path.Combine(_mockOneDriveRoot, "IntegrityTest");
            
            Directory.CreateDirectory(_testFolderPath);
        }

        public Task<string> CreateTestFolderAsync()
        {
            Console.WriteLine("Setting up mock OneDrive test folder...");
            
            if (Directory.Exists(_testFolderPath))
            {
                Console.WriteLine("Mock test folder ready: IntegrityTest");
            }
            else
            {
                Directory.CreateDirectory(_testFolderPath);
                Console.WriteLine("Created mock test folder: IntegrityTest");
            }
            
            return Task.FromResult("mock-integrity-test-folder");
        }

        public Task<string> UploadFileAsync(string localFilePath, string folderId)
        {
            Console.WriteLine($"Mock uploading file: {Path.GetFileName(localFilePath)}");
            
            string fileName = Path.GetFileName(localFilePath);
            string mockUploadPath = Path.Combine(_testFolderPath, fileName);
            
            File.Copy(localFilePath, mockUploadPath, overwrite: true);
            
            Console.WriteLine($"Mock upload successful: {fileName}");
            return Task.FromResult(fileName);
        }

        public Task<byte[]> DownloadFileAsync(string fileName, string folderId)
        {
            Console.WriteLine($"Mock downloading file: {fileName}");
            
            string mockFilePath = Path.Combine(_testFolderPath, fileName);
            
            if (!File.Exists(mockFilePath))
            {
                throw new FileNotFoundException($"Mock file not found: {fileName}");
            }
            
            byte[] fileContent = File.ReadAllBytes(mockFilePath);
            Console.WriteLine($"Mock download successful: {fileName}");
            
            return Task.FromResult(fileContent);
        }
    }
}