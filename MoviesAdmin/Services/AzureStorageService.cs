using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace MoviesAdmin.Services
{
    public class AzureStorageService : IAzureStorageService
    {
        private readonly IConfiguration _configureation;
        private readonly BlobServiceClient _blobServiceClient;

        private string containerName;

        public AzureStorageService(IConfiguration configuration)
        {

            _configureation = configuration;

            containerName = _configureation["AzureStorage:ContainerName"] ?? throw new InvalidOperationException("Missing config: AzureStorage:ContainerName");

            string connectionString = _configureation["AzureStorage:ConnectionString"] ?? throw new InvalidOperationException("Missing configuration: AzureStorage:ConnectionString");

            _blobServiceClient = new BlobServiceClient(connectionString);

        }

        //return the image file URL
        public async Task<string> UploadFileAsync(IFormFile file, string filename)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            var blobClient = containerClient.GetBlobClient(filename);

            var options = new BlobUploadOptions

            {
                HttpHeaders = new BlobHttpHeaders
                {
                    ContentType = file.ContentType
                }
            };

            // file I/O
            using (var stream = file.OpenReadStream())
            {
                await blobClient.UploadAsync(stream, options);
            }

            return blobClient.Uri.ToString();
        }
    }
}
