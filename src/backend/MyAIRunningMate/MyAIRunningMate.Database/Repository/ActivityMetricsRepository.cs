using MyAIRunningMate.Database.Entities;
using MyAIRunningMate.Database.Mappers;
using MyAIRunningMate.Domain.Interfaces.Repositories;
using MyAIRunningMate.Domain.Models;

namespace MyAIRunningMate.Database.Repository;

public class ActivityMetricsRepository(Supabase.Client supabase, IActivityMetricsRepository activityMetricsRepository) : 
    BaseRepository<ActivityMetricsEntity>(supabase), IActivityMetricsRepository
{
    private readonly Supabase.Client _supabase = supabase;

    public async Task InsertActivityMetrics(IEnumerable<ActivityMetrics> activityMetrics)
    {
        var entities = activityMetrics.Select(am => am.ToEntity());
        
        var tasks = entities.Select(entity => _supabase.From<ActivityMetricsEntity>().Insert(entity));
    
        await Task.WhenAll(tasks);
    }
}