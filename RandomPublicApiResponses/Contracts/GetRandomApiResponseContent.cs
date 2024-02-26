using RandomPublicApiResponses.Queries.GetRandomApiResponseWithTimePeriod;

namespace RandomPublicApiResponses.Contracts
{
    public class GetRandomApiResponseContent
    {
        private readonly IMediator _mediator;
        private readonly ILogger<GetRandomApiResponseContent> _logger;

        public GetRandomApiResponseContent(IMediator mediator, ILogger<GetRandomApiResponseContent> log)
        {
            _mediator = mediator;
            _logger = log;
        }

        [FunctionName("GetRandomApiResponseContent")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "getRandomApiResponseContent" })]
        [OpenApiParameter(name: "filename", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "Filename")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "The OK response")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            // would be nice to add some error handling here
            string filename = req.Query["filename"];

            var result = await _mediator.Send(new GetRandomApiResponseContentQuery(filename));

            return new OkObjectResult(result);
        }
    }
}

