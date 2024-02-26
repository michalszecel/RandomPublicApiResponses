namespace RandomPublicApiResponses.Repositories;

public interface IBlobStorageRepository
{
    Task UploadBlobAsync(string filename, string stringContent);
    Task<string> GetFileContent(string filename);
}