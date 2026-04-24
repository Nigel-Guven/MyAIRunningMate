using MyAIRunningMate.Domain.Entities;
using MyAIRunningMate.Domain.Interfaces.Infrastructure.Garmin;
using Supabase;

namespace MyAIRunningMate.Database.Repository;

public class LapRepository(Client supabase) : BaseRepository<LapEntity>(supabase), ILapRepository
{
    private readonly Client _supabase = supabase;
    
    public async Task<IEnumerable<LapEntity>> GetAllLaps()
    {
        return await GetAll();
    }

    public async Task<IEnumerable<LapEntity>> GetAllLapsByActivityId(Guid activityId)
    {
        var result = await _supabase
            .From<LapEntity>()
            .Where(x => x.ActivityId == activityId)
            .Get();

        return result.Models;
    }
}