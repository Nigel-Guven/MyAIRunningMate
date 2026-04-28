using MyAIRunningMate.Domain.Entities;
using MyAIRunningMate.Domain.Interfaces.Repositories.Garmin;
using MyAIRunningMate.Domain.Interfaces.Repositories.Strava;
using MyAIRunningMate.Domain.Interfaces.Services;
using MyAIRunningMate.Domain.Models.DTO;

namespace MyAIRunningMate.Application.Aggregations;

public class CalendarService : ICalendarService
{
    private readonly IActivityRepository _activityRepository;
    private readonly ILapRepository _lapRepository;
    private readonly IStravaResourceRepository _stravaResourceRepository;
    private readonly IStravaResourceMapRepository _stravaResourceMapRepository;
    
    public CalendarService(
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
    
    public async Task<IEnumerable<AggregateArtifactDto>> GetMonthlyAggregates(DateTime byMonth)
    {
        var activities = await _activityRepository.GetAllActivitiesByMonth(byMonth);
        
        var stravaIds = activities
            .Where(a => a.StravaResourceId != null)
            .Select(a => a.StravaResourceId!.Value)
            .ToList();
        
        var stravaResources = await _stravaResourceRepository.GetAllStravaResourcesByIds(stravaIds);

        var stravaLookup = stravaResources.ToDictionary(s => s.ResourceId, s => s);
        
        var aggregates = activities.Select(activity => 
        {
            StravaResourceEntity? stravaEntity = null;
            if (activity.StravaResourceId.HasValue)
            {
                stravaLookup.TryGetValue(activity.StravaResourceId.Value, out stravaEntity);
            }

            var garminDto = MapGarminActivityDto(activity, null); 
            var stravaDto = stravaEntity != null ? MapStravaResourceDto(stravaEntity, null) : null;

            return CreateAggregateArtifactDto(garminDto, stravaDto!);
        });

        return aggregates;
    }

    public async Task<AggregateArtifactDto?> GetAggregateActivity(Guid activityId)
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

        StravaGeomapDto? mapDto = null;

        if (stravaEntity != null)
        {
            var mapEntity = await _stravaResourceMapRepository.GetMapById(stravaEntity.ResourceId);
            if (mapEntity != null)
            {
                mapDto = MapMapResourceDto(mapEntity);
            }
        }
        
        var lapDtos = lapEntities?.Select(MapLapDto).ToList();
    
        var garminDto = MapGarminActivityDto(activityEntity, lapDtos);

        StravaResourceDto? stravaDto = null;
        if (stravaEntity != null)
        {
            stravaDto = MapStravaResourceDto(stravaEntity, mapDto);
        }

        return CreateAggregateArtifactDto(garminDto, stravaDto);
    }
}