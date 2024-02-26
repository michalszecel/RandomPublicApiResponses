using RandomPublicApiResponses.Models;

namespace RandomPublicApiResponses.Queries.GetRandomApiResponseWithTimePeriod;

public record GetRandomApiResponseWithTimePeriodQuery(DateTime FromDate, DateTime ToDate) : IRequest<IEnumerable<RandomApiResponseModel>>;
