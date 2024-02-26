using RandomPublicApiResponses.Queries.GetRandomApiResponseWithTimePeriod;
using RandomPublicApiResponses.Repositories;

namespace RandomPublicApiResponses.Contracts;

public class GetRandomApiResponseContentQueryHandler : IRequestHandler<GetRandomApiResponseContentQuery, string>
{
    private readonly IBlobStorageRepository _blobStorageRepository;

    public GetRandomApiResponseContentQueryHandler(IBlobStorageRepository blobStorageRepository)
    {
        _blobStorageRepository = blobStorageRepository;
    }

    // To simplify I am assuming that the caller already knows the filename of the content. 
    // If not we would need to extend repository to first get table record and fetch filename from there
    public async Task<string> Handle(GetRandomApiResponseContentQuery request, CancellationToken cancellationToken)
    {
        return await _blobStorageRepository.GetFileContent(request.Filename);
    }
}
