using MyAIRunningMate.Domain.Interfaces.Repositories;
using Supabase.Postgrest;
using Supabase.Postgrest.Models;

namespace MyAIRunningMate.Database.Repository;

public abstract class BaseRepository<T>(Supabase.Client supabase) : IBaseRepository<T>
    where T : BaseModel, new()
{
    protected readonly Supabase.Client Supabase = supabase;
    
    public async Task<IEnumerable<T>> GetAll()
    {
        var result = await Supabase.From<T>().Get();
        return result.Models;
    }

    public async Task<T?> GetById(object id)
    {
        return await Supabase.From<T>()
            .Filter("id", Constants.Operator.Equals, id)
            .Single();
    }

    public async Task<T> Insert(T entity)
    {

        var result = await Supabase
            .From<T>()
            .Insert(entity, new QueryOptions { Returning = QueryOptions.ReturnType.Representation });

        return result.Model ?? throw new Exception($"Failed to insert entity {typeof(T).Name}.");
    }

    public async Task<IEnumerable<T>> BulkInsert(List<T> entities)
    {
        var result = await Supabase.From<T>().Insert(entities);
        return result.Models;
    }

    public async Task Upsert(T entity)
    {
        await Supabase.From<T>().Upsert(entity);
    }
    
    public async Task Update(T entity) => await entity.Update<T>();
    public async Task Delete(T entity) => await entity.Delete<T>();
}