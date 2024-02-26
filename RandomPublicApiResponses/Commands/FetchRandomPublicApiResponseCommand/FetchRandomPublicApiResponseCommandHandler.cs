using RandomPublicApiResponses.Models;
using RandomPublicApiResponses.Repositories;
using RandomPublicApiResponses.Services;

namespace RandomPublicApiResponses.Commands.FetchRandomPublicApiResponse;

public class FetchRandomPublicApiResponseCommandHandler : IRequestHandler<FetchRandomPublicApiResponseCommand, RandomApiResponseModel>
{
    private readonly IHttpClientService _httpClient;
    private readonly IGenericDataTableRepository<RandomApiResponseModel> _azureTableService;
    private readonly IBlobStorageRepository _blobStorageRepository;

    public FetchRandomPublicApiResponseCommandHandler(IHttpClientService httpClient, IGenericDataTableRepository<RandomApiResponseModel> azureTableService,
        IBlobStorageRepository blobStorageRepository)
    {
        _azureTableService = azureTableService;
        _blobStorageRepository = blobStorageRepository;
        _httpClient = httpClient;
    }

    public async Task<RandomApiResponseModel> Handle(FetchRandomPublicApiResponseCommand request, CancellationToken cancellationToken)
    {
        var timestamp = DateTime.UtcNow;    // it's good to use standarized dateTimes within whole system
        var responseModel = new RandomApiResponseModel
        {
            PartitionKey = timestamp.Date.ToString(),       // maybe we should use something more clever here
                                                            // However I think partitioning by date is a good start
            RowKey = Guid.NewGuid().ToString(),
            Timestamp = timestamp,
            ETag = ETag.All,
        };

        try
        {
            var response = await _httpClient.GetAsync("https://api.publicapis.org/random?auth=null");
            var content = await response.Content.ReadAsStringAsync();

            responseModel.ContentFilename = $"{timestamp:yyyyMMddHHmmss}_{Guid.NewGuid()}.json";
            await _blobStorageRepository.UploadBlobAsync(responseModel.ContentFilename, content);

            responseModel.StatusCode = response.StatusCode;
        }
        catch (Exception e)
        {
            responseModel.Exception = e;
        }

        await _azureTableService.InsertEntity(responseModel);

        return responseModel;
    }
}
