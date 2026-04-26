using MyAIRunningMate.Domain.Entities;
using MyAIRunningMate.Domain.Interfaces.Infrastructure.Garmin;
using MyAIRunningMate.Domain.Interfaces.Infrastructure.Strava;
using MyAIRunningMate.Domain.Interfaces.Services;
using MyAIRunningMate.Domain.Models;
using MyAIRunningMate.Domain.Models.Activities;

namespace MyAIRunningMate.Application.Aggregations;

public class AggregatorMapper : IAggregatorMapper
{
    private readonly IActivityRepository _activityRepository;
    private readonly ILapRepository _lapRepository;
    private readonly IStravaResourceRepository _stravaResourceRepository;
    private readonly IStravaResourceMapRepository _stravaResourceMapRepository;
    
    public AggregatorMapper(
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

        MapDto? mapDto = null;

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

    public AggregateArtifactDto CreateAggregateArtifactDto(ActivityDto garminActivityDto,
        StravaResourceDto stravaActivityDto)
    {
        return new AggregateArtifactDto()
        {
            ActivityId = garminActivityDto.ActivityId,
            ResourceId = stravaActivityDto.ResourceId,
            GarminActivityId = garminActivityDto.GarminActivityId,
            StravaId = stravaActivityDto.StravaId,
            Name = stravaActivityDto.Name,
            ExerciseType = stravaActivityDto.Type,
            StartDate = stravaActivityDto.StartDate,
            StartTime = garminActivityDto.StartTime,
            ElapsedTime = stravaActivityDto.ElapsedTime,
            AverageCadence =  stravaActivityDto.AverageCadence,
            AverageSecondPerKilometre =  garminActivityDto.AverageSecondPerKilometre,
            TotalElevationGain = garminActivityDto.TotalElevationGain ?? 0.0,
            ElevationLow = stravaActivityDto.ElevationLow,
            ElevationHigh = stravaActivityDto.ElevationHigh,
            DurationSeconds = garminActivityDto.DurationSeconds,
            DistanceMetres = garminActivityDto.DistanceMetres,
            AverageHeartRate = garminActivityDto.AverageHeartRate,
            MaxHeartRate = garminActivityDto.MaxHeartRate,
            TrainingEffect = garminActivityDto.TrainingEffect,
            AchievementCount = stravaActivityDto.AchievementCount,
            KudosCount = stravaActivityDto.KudosCount,
            PersonalRecordCount =  stravaActivityDto.PersonalRecordCount,
            AthleteCount = stravaActivityDto.AthleteCount,
            Map = stravaActivityDto.Map,
            Laps = garminActivityDto.Laps,
            
        };
    }

    public StravaResourceDto MapStravaResourceDto(StravaResourceEntity stravaResourceEntity, MapDto map)
    {
        return new StravaResourceDto()
        {
            ResourceId = stravaResourceEntity.ResourceId,
            StravaId = stravaResourceEntity.StravaId,
            Name = stravaResourceEntity.Name,
            ElapsedTime = stravaResourceEntity.ElapsedTime,
            DistanceMetres = stravaResourceEntity.DistanceMetres,
            TotalElevationGain = stravaResourceEntity.TotalElevationGain,
            AverageCadence = stravaResourceEntity.AverageCadence,
            Type = stravaResourceEntity.Type,
            StartDate = stravaResourceEntity.StartDate,
            AchievementCount = stravaResourceEntity.AchievementCount,
            KudosCount = stravaResourceEntity.KudosCount,
            AthleteCount = stravaResourceEntity.AthleteCount,
            PersonalRecordCount = stravaResourceEntity.PersonalRecordCount,
            ElevationHigh = stravaResourceEntity.ElevationHigh,
            ElevationLow = stravaResourceEntity.ElevationLow,
            Map = map
        };
    }

    public ActivityDto MapGarminActivityDto(ActivityEntity entity, IEnumerable<LapDto> laps)
    {
        return new ActivityDto()
        {
            ActivityId = entity.ActivityId,
            GarminActivityId = entity.GarminActivityId,
            StartTime = entity.StartTime,
            ExerciseType = entity.ExerciseType,
            DurationSeconds = entity.DurationSeconds,
            DistanceMetres = entity.DistanceMetres,
            AverageHeartRate = entity.AverageHeartRate,
            MaxHeartRate = entity.MaxHeartRate,
            TotalElevationGain = entity.TotalElevationGain,
            AverageSecondPerKilometre = entity.AverageSecondPerKilometre,
            TrainingEffect = entity.TrainingEffect,
            StravaResourceId = entity.StravaResourceId ?? null,
            Laps = laps != null && laps.Any() ? laps : null
        };
    }

    public LapDto MapLapDto(LapEntity entity)
    {
        if (entity == null) return null;
        
        return new LapDto()
        {
            LapId = entity.LapId,
            LapNumber =  entity.LapNumber,
            Distance = entity.DistanceMetres,
            AverageHeartRate = entity.AverageHeartRate,
            Duration = entity.DurationSeconds,
        };
    }

    public MapDto MapMapResourceDto(StravaResourceMapEntity entity)
    {
        if (entity == null) return null;
        
        return new MapDto()
        {
            MapId = entity.MapId,
            MapPolyline = entity.MapPolyline,
        };
    }
}