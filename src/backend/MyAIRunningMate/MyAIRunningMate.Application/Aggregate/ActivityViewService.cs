using MyAIRunningMate.Application.Activities;
using MyAIRunningMate.Domain.Interfaces.Repositories;
using MyAIRunningMate.Domain.Models;

namespace MyAIRunningMate.Application.Aggregate;

public class ActivityViewService(
    IActivityService activityService,
    ILapRepository lapRepo,
    ITimeSeriesRecordRepository timeSeriesRepo)
    : IActivityViewService
{
    public async Task<AggregateArtifact?> CreateAggregateActivity(Guid activityId, Guid userId)
    {
        var activity = await activityService.GetByActivityIdAndUserIdAsync(activityId, userId);
    
        if (activity == null) return null;
        
        var laps = await lapRepo.GetAllLapsByActivityId(activity.ActivityId);
        
        var timeSeriesRecords = await timeSeriesRepo.GetTimeSeriesRecordsByActivityId(activity.ActivityId);
        
        return new AggregateArtifact( activity, [], laps, null, null);
    }
}