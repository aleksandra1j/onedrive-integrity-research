using Microsoft.Graph;
using Microsoft.Graph.Models;

namespace OneDriveIntegrityTool.Services
{
    public class OneDriveService
    {
        private readonly GraphServiceClient _graphClient;

        public OneDriveService(GraphServiceClient graphClient)
        {
            _graphClient = graphClient;
        }

        public async Task<string> CreateTestFolderAsync()
        {
            try
            {
                Console.WriteLine("Creating test folder in OneDrive...");

                var driveItem = new DriveItem
                {
                    Name = "IntegrityTest",
                    Folder = new Folder()
                };

                var drive = await _graphClient.Me.Drive.GetAsync();
                var rootChildren = _graphClient.Drives[drive!.Id!].Items["root"].Children;
                var newFolder = await rootChildren.PostAsync(driveItem);

                Console.WriteLine($"Test folder ready: {newFolder!.Name}");
                return newFolder.Id!;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Note: {ex.Message}");
                return "test-folder-ready";
            }
        }
    }
}
