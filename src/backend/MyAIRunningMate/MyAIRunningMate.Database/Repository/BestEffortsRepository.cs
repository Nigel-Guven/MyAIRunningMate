using MyAIRunningMate.Domain.DatabaseEntities;
using MyAIRunningMate.Domain.Interfaces.Repositories.BestEfforts;
using Supabase.Postgrest;

namespace MyAIRunningMate.Database.Repository;

public class BestEffortsRepository(Supabase.Client supabase) : BaseRepository<BestEffortEntity>(supabase), IBestEffortsRepository
{
    private readonly Supabase.Client _supabase = supabase;
    
    public async Task<IEnumerable<BestEffortEntity>> GetBestEffortsByUserId(Guid userId)
    {
        var entities = await _supabase
            .From<BestEffortEntity>()
            .Where(x => x.UserId == userId)
            .Order("distance_metres", Constants.Ordering.Ascending)
            .Get();

        return entities.Models;
    }

    public async Task UpdateBestEffort(string distanceLabel, DateTime newDate, int newTime, Guid userId)
    {
        var existingBestEffort = await _supabase
            .From<BestEffortEntity>()
            .Where(x => x.UserId == userId)
            .Where(x => x.DistanceLabel == distanceLabel)
            .Single();

        if (existingBestEffort == null)
        {
            return;
        }

        existingBestEffort.TimeAchievement = newTime;
        existingBestEffort.AchievementDate = newDate;

        await _supabase
            .From<BestEffortEntity>()
            .Update(existingBestEffort);
        
    }
}