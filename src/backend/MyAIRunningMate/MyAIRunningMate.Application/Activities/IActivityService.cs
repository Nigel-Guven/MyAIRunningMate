using MyAIRunningMate.Domain.Models;

namespace MyAIRunningMate.Application.Activities;

public interface IActivityService
{
    Task<Activity?> GetByActivityIdAndUserIdAsync(Guid activityId, Guid userId);
    Task<bool> CheckDuplicateAsync(string garminActivityId, Guid userId);
    Task SaveActivity(Activity activity);
    Task SaveLaps(IEnumerable<Lap> laps);
    Task SaveBestEfforts(IEnumerable<BestEffort>? bestEfforts);
    Task SaveTimeSeriesRecords(IEnumerable<TimeSeriesRecord>? timeSeriesRecords, Guid activityId);
    Task SaveActivityMetrics(IEnumerable<ActivityMetrics> activityMetrics);

}