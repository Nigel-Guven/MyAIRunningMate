using MyAIRunningMate.Domain.Entities;
using MyAIRunningMate.Domain.Providers.PythonFitApi.Responses;

namespace MyAIRunningMate.Domain.Interfaces.Services;

public interface IActivityService
{
    Task<ActivityEntity?> CheckDuplicateAsync(string garminActivityId);

    Task SaveActivityAndLaps(PythonAPIActivityResponse activityResponse, Guid? stravaResourceId);
}