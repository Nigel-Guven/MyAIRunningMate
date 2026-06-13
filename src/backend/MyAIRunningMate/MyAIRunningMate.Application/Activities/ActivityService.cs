using MyAIRunningMate.Domain.Interfaces.Repositories;
using MyAIRunningMate.Domain.Models;

namespace MyAIRunningMate.Application.Activities;

public class ActivityService(
    IActivityRepository activityRepository,
    ILapRepository lapRepository)
    : IActivityService
{

    public async Task<Activity?> GetByActivityIdAndUserIdAsync(Guid activityId, Guid userId) => await activityRepository.GetActivityByActivityId(activityId, userId);

    public async Task<bool> CheckDuplicateAsync(string garminActivityId, Guid userId) => 
        await activityRepository.ActivityExistsByGarminId(garminActivityId, userId);

    public async Task SaveActivityAndLaps(Activity activity, IEnumerable<Lap> laps, Guid stravaResourceId, Guid userId)
    {
        var savedActivity = await activityRepository.InsertAsync(activity, stravaResourceId, userId);
        
        await lapRepository.BulkInsertAsync(laps, savedActivity.ActivityId);
    }
}