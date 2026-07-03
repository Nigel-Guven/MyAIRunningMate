
using MyAIRunningMate.Domain.Interfaces.Repositories;
using MyAIRunningMate.Domain.Models;

namespace MyAIRunningMate.Application.Activities;

public class ActivityService(
    IActivityRepository activityRepository, 
    IActivityMetricsRepository activityMetricsRepository, 
    ILapRepository lapRepository, 
    IBestEffortsRepository bestEffortsRepository, 
    ITimeSeriesRecordRepository timeSeriesRecordRepository)
    : IActivityService
{

    public async Task<Activity?> GetByActivityIdAndUserIdAsync(Guid activityId, Guid userId) => await activityRepository.GetActivityByActivityId(activityId, userId);

    public async Task<bool> CheckDuplicateAsync(string garminActivityId, Guid userId) 
        => await activityRepository.ActivityExistsByGarminId(garminActivityId, userId);

    public async Task SaveActivity(Activity activity) => await activityRepository.InsertAsync(activity);

    public async Task SaveLaps(IEnumerable<Lap> laps) => await lapRepository.InsertLaps(laps);

    public async Task SaveBestEfforts(IEnumerable<BestEffort>? bestEfforts) 
        => await bestEffortsRepository.InsertBestEfforts(bestEfforts);

    public async Task SaveTimeSeriesRecords(IEnumerable<TimeSeriesRecord>? timeSeriesRecords, Guid activityId)
        => await timeSeriesRecordRepository.InsertAsync(timeSeriesRecords, activityId);

    public async Task SaveActivityMetrics(ActivityMetrics activityMetrics) 
        => await activityMetricsRepository.InsertActivityMetrics(activityMetrics);
}