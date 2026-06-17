
using MyAIRunningMate.Domain.Interfaces.Repositories;
using MyAIRunningMate.Domain.Models;

namespace MyAIRunningMate.Application.Activities;

public class ActivityService(
    IActivityRepository activityRepository)
    : IActivityService
{

    public async Task<Activity?> GetByActivityIdAndUserIdAsync(Guid activityId, Guid userId) => await activityRepository.GetActivityByActivityId(activityId, userId);

    public async Task<bool> CheckDuplicateAsync(string garminActivityId, Guid userId) => 
        await activityRepository.ActivityExistsByGarminId(garminActivityId, userId);

    public async Task SaveActivityAndLaps(Activity activity, IEnumerable<Lap> laps, Guid userId) => await activityRepository.InsertAsync(activity, laps);
}