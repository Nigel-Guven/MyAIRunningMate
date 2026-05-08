
using MyAIRunningMate.Application.Models;

namespace MyAIRunningMate.Application.Activities;

public interface IActivityService
{
    Task<bool> CheckDuplicateAsync(string garminActivityId, Guid userId);

    Task SaveActivityAndLaps(Activity activity, Guid? stravaResourceId, Guid userId);
}