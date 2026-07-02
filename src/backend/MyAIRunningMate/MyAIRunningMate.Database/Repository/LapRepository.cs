using MyAIRunningMate.Database.Entities;
using MyAIRunningMate.Database.Mappers;
using MyAIRunningMate.Domain.Interfaces.Repositories;
using MyAIRunningMate.Domain.Models;

namespace MyAIRunningMate.Database.Repository;

public class LapRepository(Supabase.Client supabase) : BaseRepository<LapEntity>(supabase), ILapRepository
{
    private readonly Supabase.Client _supabase = supabase;
    
    public async Task<IEnumerable<Lap>> GetAllLaps()
    {
        var entities = await GetAllAsync();

        return entities.Select(entity => entity.ToDomain());
    }

    public async Task<IEnumerable<Lap>> GetAllLapsByActivityId(Guid activityId)
    {
        var result = await _supabase
            .From<LapEntity>()
            .Where(x => x.ActivityId == activityId)
            .Get();
        
        return result.Models.Select(entity => entity.ToDomain());
    }

    public async Task InsertLaps(IEnumerable<Lap> laps) 
    {
        var entities = laps.Select(lap => lap.ToEntity());
        
        var tasks = entities.Select(entity => _supabase.From<LapEntity>().Insert(entity));
    
        await Task.WhenAll(tasks);
    }
}