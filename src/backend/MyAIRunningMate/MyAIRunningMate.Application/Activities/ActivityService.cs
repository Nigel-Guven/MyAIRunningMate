using MyAIRunningMate.Application.Models;
using MyAIRunningMate.Database.DbEntityMappings;
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
    
    public async Task<bool> CheckDuplicateAsync(string garminActivityId)
    {
        return await _activityRepository.ActivityExistsByGarminId(garminActivityId);
    }

    public async Task SaveActivityAndLaps(Activity activity, Guid? stravaResourceId)
    {
        var activityEntity = activity.ToActivityEntity(stravaResourceId);
        var lapEntities = activity.Laps.Select(l => l.ToLapEntity()).ToList();
        
        await _activityRepository.Insert(activityEntity);

        await _lapRepository.BulkInsert(lapEntities);
    }
}