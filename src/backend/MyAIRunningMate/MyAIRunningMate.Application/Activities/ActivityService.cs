using MyAIRunningMate.Application.DbEntityMappings;
using MyAIRunningMate.Application.Models;
using MyAIRunningMate.Domain.Interfaces.Repositories.Garmin;

namespace MyAIRunningMate.Application.Activities;

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
    
    public async Task<bool> CheckDuplicateAsync(string garminActivityId, Guid userId)
    {
        return await _activityRepository.ActivityExistsByGarminId(garminActivityId, userId);
    }

    public async Task SaveActivityAndLaps(Activity activity, Guid? stravaResourceId, Guid userId)
    {
        var activityEntity = activity.ToActivityEntity(stravaResourceId, userId);
        
        var result = await _activityRepository.Insert(activityEntity);

        var lapEntities = activity.Laps.Select(l => 
        {
            var entity = l.ToLapEntity();
            entity.ActivityId = result.ActivityId;
            return entity;
        }).ToList();
        
        await _lapRepository.BulkInsert(lapEntities);
    }
}