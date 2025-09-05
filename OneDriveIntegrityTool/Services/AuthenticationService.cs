using Microsoft.Graph;
using Azure.Identity;


namespace OneDriveIntegrityTool.Services
{
    public class AuthenticationService
    {
        private readonly string[] _scopes={ "https://graph.microsoft.com/Files.ReadWrite" };

       public async Task<GraphServiceClient> GetGraphClientAsync()
        {
            var options = new DeviceCodeCredentialOptions
            {
                ClientId = "14d82eec-204b-4c2f-b7e8-296a70dab67e",
                TenantId = "common",
                DeviceCodeCallback = (code, cancellation) =>
                {
                    Console.WriteLine($"Go to: {code.VerificationUri}");
                    Console.WriteLine($"Enter code: {code.UserCode}");
                    Console.WriteLine("Waiting to complete authentication in browser...");
                    return Task.CompletedTask;
                }
            };

            var deviceCodeCredential = new DeviceCodeCredential(options);
            var graphClient = new GraphServiceClient(deviceCodeCredential, _scopes);
            
            await graphClient.Me.Drive.GetAsync();
            return graphClient;
        }
    
    }
}