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
                var authService = new AuthenticationService();
                Console.WriteLine("Connecting to OneDrive...");
                
                var graphClient = await authService.GetGraphClientAsync();
                
                Console.WriteLine("Success! Authentication and OneDrive access confirmed.");
                Console.WriteLine("Ready to start file integrity testing!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }

}