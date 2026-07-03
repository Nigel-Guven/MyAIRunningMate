using MyAIRunningMate.Database.Entities;
using MyAIRunningMate.Database.Mappers;
using MyAIRunningMate.Domain.Interfaces.Repositories;
using MyAIRunningMate.Domain.Models;

namespace MyAIRunningMate.Database.Repository;

public class ActivityMetricsRepository(Supabase.Client supabase) : 
    BaseRepository<ActivityMetricsEntity>(supabase), IActivityMetricsRepository
{
    private readonly Supabase.Client _supabase = supabase;

    public async Task<ActivityMetrics> GetActivityMetrics(Guid activityId)
    {
        var result = await _supabase
            .From<ActivityMetricsEntity>() 
            .Where(x => x.ActivityId == activityId)
            .Get(); 

        return result.Model.ToDomain();
    }

    public async Task InsertActivityMetrics(ActivityMetrics activityMetrics)
    {
        var entity = activityMetrics.ToEntity();
        
        await _supabase.From<ActivityMetricsEntity>().Insert(entity);
    }
}