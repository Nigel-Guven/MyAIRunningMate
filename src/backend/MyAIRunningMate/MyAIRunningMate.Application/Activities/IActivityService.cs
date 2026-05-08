using MyAIRunningMate.Application.Models;
using MyAIRunningMate.Domain.DatabaseEntities;

namespace MyAIRunningMate.Application.Activities;

public interface IActivityService
{
    Task<ActivityEntity?> GetByActivityIdAndUserIdAsync(Guid activityId, Guid userId);
    
    Task<bool> CheckDuplicateAsync(string garminActivityId, Guid userId);

    Task SaveActivityAndLaps(Activity activity, Guid? stravaResourceId, Guid userId);
}