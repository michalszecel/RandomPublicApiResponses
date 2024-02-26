namespace RandomPublicApiResponses.Queries.GetRandomApiResponseWithTimePeriod;

public record GetRandomApiResponseContentQuery(string Filename) : IRequest<string>;
