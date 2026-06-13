using Supabase.Postgrest.Models;

namespace MyAIRunningMate.Database;

public interface IBaseRepository<T> where T : BaseModel, new()
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetByIdAsync(object id);
    Task<T> InsertAsync(T entity);
    Task<IEnumerable<T>> BulkInsertAsync(List<T> entities);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
}