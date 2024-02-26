using RandomPublicApiResponses.Queries.GetRandomApiResponseWithTimePeriod;

namespace RandomPublicApiResponses.Contracts
{
    public class GetRandomApiResponsesWithinTimeRange
    {
        private readonly IMediator _mediator;
        private readonly ILogger<GetRandomApiResponsesWithinTimeRange> _logger;

        public GetRandomApiResponsesWithinTimeRange(IMediator mediator, ILogger<GetRandomApiResponsesWithinTimeRange> log)
        {
            _mediator = mediator;
            _logger = log;
        }

        [FunctionName("GetRandomApiResponsesWithinTimeRange")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "getRandomApiResponsesWithinTimeRange" })]
        [OpenApiParameter(name: "fromTime", In = ParameterLocation.Query, Required = true, Type = typeof(DateTime), Description = "From time")]
        [OpenApiParameter(name: "toTime", In = ParameterLocation.Query, Required = true, Type = typeof(DateTime), Description = "To time")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "The OK response")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            // would be nice to add some error handling here
            DateTime fromTime = DateTime.Parse(req.Query["fromTime"]);
            DateTime toTime = DateTime.Parse(req.Query["toTime"]);

            var results = await _mediator.Send(new GetRandomApiResponseWithTimePeriodQuery(fromTime, toTime));
            
            return new OkObjectResult(results);
        }
    }
}

