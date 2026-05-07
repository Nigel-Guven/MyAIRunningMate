using MyAIRunningMate.Application.Models.ViewObjects;
using MyAIRunningMate.Domain.DatabaseEntities;
using MyAIRunningMate.Domain.Interfaces.Repositories.Garmin;
using MyAIRunningMate.Domain.Interfaces.Repositories.Strava;
using MyAIRunningMate.Domain.Interfaces.Services;

namespace MyAIRunningMate.Application.UserInterface;

public class ActivityViewService : IActivityViewService
{
    private readonly IActivityRepository _activityRepository;
    private readonly ILapRepository _lapRepository;
    private readonly IStravaResourceRepository _stravaResourceRepository;
    private readonly IStravaResourceMapRepository _stravaResourceMapRepository;
    
    public ActivityViewService(
        IActivityRepository activityRepo,
        ILapRepository lapRepo,
        IStravaResourceRepository stravaRepo,
        IStravaResourceMapRepository mapRepo)
    {
        _activityRepository = activityRepo;
        _lapRepository = lapRepo;
        _stravaResourceRepository = stravaRepo;
        _stravaResourceMapRepository = mapRepo;
    }
    

    public async Task<AggregateArtifactView> CreateAggregateActivity(Guid activityId)
    {
        var activityEntity = await _activityRepository.GetById(activityId);
    
        if (activityEntity == null) return null;
        
        var lapsTask = _lapRepository.GetAllLapsByActivityId(activityId);
        
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
                var mapEntity = await _stravaResourceMapRepository.GetMapById(stravaEntity.ResourceId);
                mapView = mapEntity.ToMapView();
            }
            
        }
        
        var activityView = activityEntity.ToActivityView();
        var lapViews = lapEntities.Select(l => l.ToLapView());

        return AggregateArtifactViewMapper.ToAggregateArtifactView(activityView, lapViews, stravaResourceView, mapView);
    }
}