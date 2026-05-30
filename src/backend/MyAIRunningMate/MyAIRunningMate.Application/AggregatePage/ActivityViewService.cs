using MyAIRunningMate.Application.Activities;
using MyAIRunningMate.Application.Models.ViewObjects;
using MyAIRunningMate.Domain.DatabaseEntities;
using MyAIRunningMate.Domain.Interfaces.Repositories.Garmin;
using MyAIRunningMate.Domain.Interfaces.Repositories.Strava;

namespace MyAIRunningMate.Application.AggregatePage;

public class ActivityViewService : IActivityViewService
{
    private readonly IActivityService _activityService;
    private readonly ILapRepository _lapRepository;
    private readonly IStravaResourceRepository _stravaResourceRepository;
    private readonly IStravaResourceMapRepository _stravaResourceMapRepository;
    
    public ActivityViewService(
        IActivityService activityService,
        ILapRepository lapRepo,
        IStravaResourceRepository stravaRepo,
        IStravaResourceMapRepository mapRepo)
    {
        _activityService = activityService;
        _lapRepository = lapRepo;
        _stravaResourceRepository = stravaRepo;
        _stravaResourceMapRepository = mapRepo;
    }
    

    public async Task<AggregateArtifactView> CreateAggregateActivity(Guid activityId, Guid userId)
    {
        var activityEntity = await _activityService.GetByActivityIdAndUserIdAsync(activityId, userId);
    
        if (activityEntity == null) return null;
        
        var lapsTask = _lapRepository.GetAllLapsByActivityId(activityEntity.ActivityId);
        
        var stravaTask = activityEntity.StravaResourceId != null 
            ? _stravaResourceRepository.GetById(activityEntity.StravaResourceId) 
            : Task.FromResult<StravaResourceEntity?>(null);

        await Task.WhenAll(lapsTask, stravaTask);
        
        var lapEntities = await lapsTask;
        var stravaEntity = await stravaTask;

        StravaResourceView stravaResourceView = null;
        StravaGeomapView? mapView = null;

        if (stravaEntity != null)
        {
            stravaResourceView = stravaEntity.ToStravaResourceView();
            
            
            if (stravaEntity.MapId != null)
            {
                var mapEntity = await _stravaResourceMapRepository.GetMapById(stravaEntity.MapId.Value);
                
                if (mapEntity != null) 
                {
                    mapView = mapEntity.ToMapView();
                }
            }
            
        }
        
        var activityView = activityEntity.ToActivityView();
        var lapViews = lapEntities.Select(l => l.ToLapView());

        return AggregateArtifactViewMapper.ToAggregateArtifactView(activityView, lapViews, stravaResourceView, mapView);
    }
}