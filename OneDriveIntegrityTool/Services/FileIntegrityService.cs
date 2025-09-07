using System.Security.Cryptography;
using System.Text;

namespace OneDriveIntegrityTool.Services
{
    public class FileIntegrityService
    {
        public string ComputeSHA256Hash(string filePath)
        {
            using var sha256 = SHA256.Create();
            using var fileStream = File.OpenRead(filePath);
            byte[] hashBytes = sha256.ComputeHash(fileStream);
            return Convert.ToHexString(hashBytes);
        }

        public string ComputeSHA256Hash(byte[] fileContent)
        {
            using var sha256 = SHA256.Create();
            byte[] hashBytes = sha256.ComputeHash(fileContent);
            return Convert.ToHexString(hashBytes);
        }

        public bool CompareFiles(string originalPath, string downloadedPath)
        {
            string originalHash = ComputeSHA256Hash(originalPath);
            string downloadedHash = ComputeSHA256Hash(downloadedPath);
            
            Console.WriteLine($"Original file hash:   {originalHash}");
            Console.WriteLine($"Downloaded file hash: {downloadedHash}");
            
            return originalHash == downloadedHash;
        }
    }
}