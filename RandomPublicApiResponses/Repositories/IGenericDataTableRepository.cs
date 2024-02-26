namespace RandomPublicApiResponses.Repositories;

public interface IGenericDataTableRepository<T>
{
    Task InsertEntity(T entity);
    Task<IEnumerable<T>> GetByTimePeriod(DateTime fromTime, DateTime toTime);
}