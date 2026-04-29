using MyAIRunningMate.Domain.Entities;
using MyAIRunningMate.Domain.Interfaces.Repositories.Garmin;
using MyAIRunningMate.Domain.Interfaces.Services;
using MyAIRunningMate.Domain.Mappers;
using MyAIRunningMate.Domain.Models.DTO;

namespace MyAIRunningMate.Application.Garmin;

public class ActivityService : IActivityService
{
    private readonly IActivityRepository _activityRepository;
    private readonly ILapRepository _lapRepository;
    
    public ActivityService(
        IActivityRepository activityRepository,
        ILapRepository lapRepository)
    {
        _activityRepository = activityRepository;
        _lapRepository = lapRepository;
    }
    
    public async Task<ActivityEntity?> CheckDuplicateAsync(string garminActivityId)
    {
        return await _activityRepository.ActivityExistsByGarminId(garminActivityId);
    }

    public async Task SaveActivityAndLaps(ActivityDto activityDto, Guid? stravaResourceId)
    {
        activityDto.StravaResourceId = stravaResourceId;

        var activityEntity = activityDto.ToEntity();
        var lapEntities = activityDto.Laps.Select(l => l.ToEntity()).ToList();
        
        await _activityRepository.Insert(activityEntity);
        await _lapRepository.BulkInsert(lapEntities);
    }
}