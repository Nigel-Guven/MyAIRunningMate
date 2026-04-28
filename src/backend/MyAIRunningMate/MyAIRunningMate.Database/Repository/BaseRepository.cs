using MyAIRunningMate.Domain.Interfaces.Repositories;
using Supabase.Postgrest;
using Supabase.Postgrest.Models;
using Client = Supabase.Client;

namespace MyAIRunningMate.Database.Repository;

public abstract class BaseRepository<T>(Client supabase) : IBaseRepository<T>
    where T : BaseModel, new()
{
    protected readonly Client _supabase = supabase;
    
    public async Task<IEnumerable<T>> GetAll()
    {
        var result = await supabase.From<T>().Get();
        return result.Models;
    }

    public async Task<T?> GetById(object id)
    {
        return await supabase.From<T>()
            .Filter("id", Constants.Operator.Equals, id)
            .Single();
    }

    public async Task<T> Insert(T entity)
    {
        var result = await supabase.From<T>().Insert(entity);
        return result.Model ?? throw new Exception($"Failed to insert entity {typeof(T).Name}.");
    }

    public async Task<IEnumerable<T>> BulkInsert(List<T> entities)
    {
        var result = await supabase.From<T>().Insert(entities);
        return result.Models;
    }

    public async Task Upsert(T entity)
    {
        await _supabase.From<T>().Upsert(entity);
    }
    
    public async Task Update(T entity) => await entity.Update<T>();
    public async Task Delete(T entity) => await entity.Delete<T>();
}