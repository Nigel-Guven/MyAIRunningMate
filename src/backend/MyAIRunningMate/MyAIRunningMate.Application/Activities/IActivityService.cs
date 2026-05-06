
using MyAIRunningMate.Application.Models;

namespace MyAIRunningMate.Application.Activities;

public interface IActivityService
{
    Task<bool> CheckDuplicateAsync(string garminActivityId);

    Task SaveActivityAndLaps(Activity activity, Guid? stravaResourceId);
}