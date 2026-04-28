
using Supabase.Postgrest.Models;

namespace MyAIRunningMate.Domain.Interfaces.Repositories;

public interface IBaseRepository<T> where T : BaseModel, new()
{
    Task<IEnumerable<T>> GetAll();
    Task<T?> GetById(object id);
    Task<T> Insert(T entity);
    Task<IEnumerable<T>> BulkInsert(List<T> entities);
    Task Update(T entity);
    Task Delete(T entity);
}