using MyAIRunningMate.Application.Activities;
using MyAIRunningMate.Domain.Interfaces.Repositories;

namespace MyAIRunningMate.Application.AggregatePage;

public class ActivityViewService(
    IActivityService activityService,
    ILapRepository lapRepo,
    IStravaResourceRepository stravaRepo,
    IStravaResourceMapRepository mapRepo)
    : IActivityViewService
{
    public async Task<AggregateArtifactView> CreateAggregateActivity(Guid activityId, Guid userId)
    {
        var activityEntity = await activityService.GetByActivityIdAndUserIdAsync(activityId, userId);
    
        if (activityEntity == null) return null;
        
        var lapsTask = lapRepo.GetAllLapsByActivityId(activityEntity.ActivityId);
        
        var stravaTask = activityEntity.StravaResourceId != null 
            ? stravaRepo.GetByIdAsync(activityEntity.StravaResourceId) 
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
                var mapEntity = await mapRepo.GetMapById(stravaEntity.MapId.Value);
                
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