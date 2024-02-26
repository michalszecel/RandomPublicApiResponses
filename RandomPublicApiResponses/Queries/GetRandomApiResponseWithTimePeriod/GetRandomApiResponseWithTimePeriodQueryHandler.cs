using RandomPublicApiResponses.Models;
using RandomPublicApiResponses.Queries.GetRandomApiResponseWithTimePeriod;
using RandomPublicApiResponses.Repositories;

namespace RandomPublicApiResponses.Contracts;

public class GetRandomApiResponseWithTimePeriodQueryHandler : IRequestHandler<GetRandomApiResponseWithTimePeriodQuery, IEnumerable<RandomApiResponseModel>>
{
    private readonly IGenericDataTableRepository<RandomApiResponseModel> _azureTableService;

    public GetRandomApiResponseWithTimePeriodQueryHandler(IGenericDataTableRepository<RandomApiResponseModel> azureTableService)
    {
        _azureTableService = azureTableService;
    }

    public async Task<IEnumerable<RandomApiResponseModel>> Handle(GetRandomApiResponseWithTimePeriodQuery request, CancellationToken cancellationToken)
    {
        return await _azureTableService.GetByTimePeriod(request.FromDate, request.ToDate);
    }
}
