using MyAIRunningMate.Database.Entities;
using MyAIRunningMate.Database.Mappers;
using MyAIRunningMate.Domain.Interfaces.Repositories;
using MyAIRunningMate.Domain.Models;
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

    public async Task UpdateBestEffort(string distanceLabel, DateTime newDate, int newTime, Guid userId)
    {
        var cleanLabel = distanceLabel?.Trim();

        await _supabase
            .From<BestEffortEntity>()
            .Where(x => x.UserId == userId)
            .Where(x => x.DistanceLabel == cleanLabel)
            .Set(x => x.TimeAchievement, newTime)
            .Set(x => x.AchievementDate, newDate)
            .Update();
    }
}