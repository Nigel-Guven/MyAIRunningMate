using MyAIRunningMate.Database.Entities;
using MyAIRunningMate.Database.Mappers;
using MyAIRunningMate.Domain.Interfaces.Repositories;
using Supabase.Postgrest;

namespace MyAIRunningMate.Database.Repository;

public class BestEffortsRepository(Supabase.Client supabase) : BaseRepository<BestEffortEntity>(supabase), IBestEffortsRepository
{
    private readonly Supabase.Client _supabase = supabase;
    
    public async Task<IEnumerable<BestEffort>> GetBestEffortsByUserId(Guid userId)
    {
        var entities = await _supabase
            .From<BestEffortEntity>()
            .Where(x => x.UserId == userId)
            .Order("distance_metres", Constants.Ordering.Ascending)
            .Get();

        return entities.Models.Select(entity => entity.ToDomain());
    }

    public Task UpdateBestEffort(string distanceLabel, DateTime newDate, int newTime, Guid userId) => throw new NotImplementedException();
    
    public async Task InsertBestEfforts(IEnumerable<BestEffort> bestEfforts)
    {
        var entities = bestEfforts.Select(am => am.ToEntity());
        
        var tasks = entities.Select(entity => _supabase.From<BestEffortEntity>().Insert(entity));
    
        await Task.WhenAll(tasks);
    }
}