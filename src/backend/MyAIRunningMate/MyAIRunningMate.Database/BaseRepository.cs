using Supabase.Postgrest;
using Supabase.Postgrest.Models;

namespace MyAIRunningMate.Database;

public abstract class BaseRepository<T>(Supabase.Client supabase) : IBaseRepository<T>
    where T : BaseModel, new()
{
    protected readonly Supabase.Client Supabase = supabase;
    
    public async Task<IEnumerable<T>> GetAllAsync()
    {
        var result = await Supabase.From<T>().Get();
        return result.Models;
    }

    public async Task<T?> GetByIdAsync(object id) =>
        await Supabase.From<T>()
            .Filter("id", Constants.Operator.Equals, id.ToString())
            .Single();

    public async Task<T> InsertAsync(T entity)
    {

        var result = await Supabase
            .From<T>()
            .Insert(entity, new QueryOptions { Returning = QueryOptions.ReturnType.Representation });

        return result.Model ?? throw new Exception($"Failed to insert entity {typeof(T).Name}.");
    }

    public async Task<IEnumerable<T>> BulkInsertAsync(List<T> entities)
    {
        var result = await Supabase.From<T>().Insert(entities);
        return result.Models;
    }

    protected async Task UpsertAsync(T entity) => await Supabase.From<T>().Upsert(entity);
    public async Task UpdateAsync(T entity) => await entity.Update<T>();
    public async Task DeleteAsync(T entity) => await entity.Delete<T>();
}