using Azure.Storage.Blobs;
using System.IO;
using System.Text;

namespace RandomPublicApiResponses.Repositories;

public class BlobStorageRepository : IBlobStorageRepository
{
    private readonly string _blobContainerName = "random-api-response-content";
    private readonly string _connectionString = "";
    // hard coded - ideally these should be in a configuration file (for local) and Azure App Config with Key Vault for secrets

    public BlobStorageRepository()
    {
    }

    public async Task UploadBlobAsync(string filename, string stringContent)
    {
        var blobClient = await GetBlobClient(filename);

        var content = Encoding.UTF8.GetBytes(stringContent);
        using (var ms = new MemoryStream(content))
        {
            await blobClient.UploadAsync(ms, true);
        }
    }

    public async Task<string> GetFileContent(string filename)
    {
        var blobClient = await GetBlobClient(filename);
        var result = await blobClient.DownloadContentAsync();
        return result.Value.Content.ToString();
    }

    private async Task<BlobClient> GetBlobClient(string filename)
    {
        var blobServiceClient = new BlobServiceClient(_connectionString);
        BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(_blobContainerName);
        await containerClient.CreateIfNotExistsAsync();

        var blobClient = containerClient.GetBlobClient(filename);
        return blobClient;
    }
}