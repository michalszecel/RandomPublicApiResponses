namespace RandomPublicApiResponses.Services;

public interface IHttpClientService
{
    Task<HttpResponseMessage> GetAsync(string url);
}