using RandomPublicApiResponses.Commands.FetchRandomPublicApiResponse;

namespace RandomPublicApiResponses.Contracts;

public class FetchRandomPublicApiResponse
{
    private readonly IMediator _mediator;

    public FetchRandomPublicApiResponse(IMediator mediator)
    {
        _mediator = mediator;
    }

    [FunctionName("fetchRandomPublicApiResponse")]
    //public void Run([TimerTrigger("0-59 * * * * *")] TimerInfo timer, ILogger log)    // every second
    public void Run([TimerTrigger("0 */1 * * * *")] TimerInfo timer, ILogger log)       // every minute
    {
        _mediator.Send(new FetchRandomPublicApiResponseCommand());
    }
}
