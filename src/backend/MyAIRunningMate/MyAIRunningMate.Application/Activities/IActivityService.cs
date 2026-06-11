using MyAIRunningMate.Domain.DatabaseEntities;
using MyAIRunningMate.Domain.Models;

namespace MyAIRunningMate.Application.Activities;

public interface IActivityService
{
    Task<ActivityEntity?> GetByActivityIdAndUserIdAsync(Guid activityId, Guid userId);
    
    Task<bool> CheckDuplicateAsync(string garminActivityId, Guid userId);

    Task SaveActivityAndLaps(Activity activity, Guid? stravaResourceId, Guid userId);
}