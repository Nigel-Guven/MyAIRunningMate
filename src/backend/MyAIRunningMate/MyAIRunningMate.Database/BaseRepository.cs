using Supabase.Postgrest;
using Supabase.Postgrest.Exceptions;
using Supabase.Postgrest.Models;

namespace MyAIRunningMate.Database;

public abstract class BaseRepository<T>(Supabase.Client supabase) : IBaseRepository<T>
    where T : BaseModel, new()
{
    public async Task<IEnumerable<T>> GetAllAsync()
    {
        var result = await supabase.From<T>().Get();
        return result.Models;
    }

    public async Task<T?> GetByIdAsync(object id) =>
        await supabase.From<T>()
            .Filter("id", Constants.Operator.Equals, id.ToString())
            .Single();

    public async Task<T> InsertAsync(T entity)
    {

        var result = await supabase
            .From<T>()
            .Insert(entity, new QueryOptions { Returning = QueryOptions.ReturnType.Representation });

        return result.Model ?? throw new Exception($"Failed to insert entity {typeof(T).Name}.");
    }

    public async Task<IEnumerable<T>> BulkInsertAsync(List<T> entities)
    {
        try
        {
            var result = await supabase.From<T>().Insert(entities);
            return result.Models;
        }
        catch (PostgrestException ex)
        {
            Console.WriteLine($"DB Error Message: {ex.Message}");
            Console.WriteLine($"DB Error Content: {ex.Content}");
            throw;
        }
    }
    
    public async Task UpdateAsync(T entity) => await entity.Update<T>();
    public async Task DeleteAsync(T entity) => await entity.Delete<T>();
}