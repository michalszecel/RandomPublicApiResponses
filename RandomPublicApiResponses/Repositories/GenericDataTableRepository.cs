namespace RandomPublicApiResponses.Repositories;

public class GenericDataTableRepository<T> : IGenericDataTableRepository<T> where T : class, ITableEntity
{
    private readonly string _tableName;
    private readonly string _connectionString = "";
    // everything hard coded - ideally these should be in a configuration file (for local) and Azure App Config with Key Vault for secrets

    public GenericDataTableRepository()
    {
        _tableName = typeof(T).Name;
    }

    public async Task InsertEntity(T entity)
    {
        var tableClient = await GetTableClient();
        await tableClient.AddEntityAsync(entity);
    }

    // it is a simplification - in such repository we should implement more generic method which would accept a linq query 
    public async Task<IEnumerable<T>> GetByTimePeriod(DateTime fromTime, DateTime toTime)
    {
        var results = new List<T>();
        var tableClient = await GetTableClient();

        var tableResults = tableClient.QueryAsync<T>(x => x.Timestamp >= fromTime && x.Timestamp <= toTime);
        // same here - we should return limited data with pagination or continuation token
        await foreach (var tableResult in tableResults)
        {
            results.Add(tableResult);
        }

        return results;
    }

    private async Task<TableClient> GetTableClient()
    {
        var tableClient = new TableClient(_connectionString, _tableName);

        await tableClient.CreateIfNotExistsAsync();
        return tableClient;
    }
}