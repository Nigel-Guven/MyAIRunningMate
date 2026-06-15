using MyAIRunningMate.Domain.Models;

namespace MyAIRunningMate.Application.Activities;

public interface IActivityService
{
    Task<Activity?> GetByActivityIdAndUserIdAsync(Guid activityId, Guid userId);
    Task<bool> CheckDuplicateAsync(string garminActivityId, Guid userId);
    Task SaveActivityAndLaps(Activity activity, IEnumerable<Lap> laps, Guid userId);
}