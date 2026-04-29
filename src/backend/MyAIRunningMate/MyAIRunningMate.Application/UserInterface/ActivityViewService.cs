using MyAIRunningMate.Domain.Entities;
using MyAIRunningMate.Domain.Interfaces.Repositories.Garmin;
using MyAIRunningMate.Domain.Interfaces.Repositories.Strava;
using MyAIRunningMate.Domain.Interfaces.Services;
using MyAIRunningMate.Domain.Mappers;
using MyAIRunningMate.Domain.Models.DTO;

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
    

    public async Task<AggregateArtifactDto?> CreateAggregateActivityDto(Guid activityId)
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
            mapDto = mapEntity?.ToDto();
        }
        
        var garminDto = activityEntity.ToDto(lapEntities);

        StravaResourceDto? stravaDto = null;
        
        if (stravaEntity != null)
        {
            stravaDto = stravaEntity.ToDto();
            stravaDto.StravaGeomap = mapDto;
        }

        return AggregateArtifactMapper.ToDto(garminDto, stravaDto);
    }
}