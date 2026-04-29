using MyAIRunningMate.Domain.Entities;
using MyAIRunningMate.Domain.Models.DTO;

namespace MyAIRunningMate.Domain.Interfaces.Services;

public interface IActivityService
{
    Task<ActivityEntity?> CheckDuplicateAsync(string garminActivityId);

    Task SaveActivityAndLaps(ActivityDto activityDto, Guid? stravaResourceId);
}