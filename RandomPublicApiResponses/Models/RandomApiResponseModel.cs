namespace RandomPublicApiResponses.Models;

public class RandomApiResponseModel : ITableEntity
{
    public Exception Exception { get; set; }
    public HttpStatusCode StatusCode { get; set; }
    public string PartitionKey { get; set; }
    public string RowKey { get; set; }
    public string ContentFilename { get; set; }
    public DateTimeOffset? Timestamp { get; set; }
    public ETag ETag { get; set; }
}