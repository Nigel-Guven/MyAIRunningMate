using MyAIRunningMate.Domain.Entities;
using MyAIRunningMate.Domain.Interfaces.Repositories.Garmin;
using MyAIRunningMate.Domain.Interfaces.Repositories.Strava;
using MyAIRunningMate.Domain.Interfaces.Services;
using MyAIRunningMate.Domain.Models;
using MyAIRunningMate.Domain.Models.DTO;

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

        GeomapDto? mapDto = null;

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
            GarminActivityId = garminActivityDto.GarminActivityId,
            StartTime = garminActivityDto.StartTime,
            DistanceMetres = garminActivityDto.DistanceMetres,
            DurationSeconds = garminActivityDto.DurationSeconds,
            TrainingEffect = garminActivityDto.TrainingEffect,
            AverageSecondPerKilometre = garminActivityDto.AverageSecondPerKilometre,
            AverageHeartRate = garminActivityDto.AverageHeartRate,
            MaxHeartRate = garminActivityDto.MaxHeartRate,
            Laps = garminActivityDto.Laps,
            
            ResourceId = stravaActivityDto?.ResourceId ?? Guid.Empty,
            StravaId = stravaActivityDto?.StravaId,
            Name = stravaActivityDto?.Name ?? "Unnamed Activity",
            
            ExerciseType = stravaActivityDto?.Type ?? garminActivityDto.ExerciseType ?? "Unknown",

            StartDate = stravaActivityDto?.StartDate ?? garminActivityDto.StartTime,
        

            ElapsedTime = stravaActivityDto?.ElapsedTime,
            AverageCadence = stravaActivityDto?.AverageCadence,
            TotalElevationGain = garminActivityDto.TotalElevationGain ?? stravaActivityDto?.TotalElevationGain ?? 0.0,
            ElevationLow = stravaActivityDto?.ElevationLow,
            ElevationHigh = stravaActivityDto?.ElevationHigh,

            AchievementCount = stravaActivityDto?.AchievementCount ?? 0,
            KudosCount = stravaActivityDto?.KudosCount ?? 0,
            PersonalRecordCount = stravaActivityDto?.PersonalRecordCount ?? 0,
            AthleteCount = stravaActivityDto?.AthleteCount ?? 0,
        
            Map = stravaActivityDto?.Geomap
        };
    }

    public StravaResourceDto MapStravaResourceDto(StravaResourceEntity stravaResourceEntity, GeomapDto geomap)
    {
        return new StravaResourceDto()
        {
            ResourceId = stravaResourceEntity.ResourceId,
            StravaId = stravaResourceEntity.StravaId,
            Name = stravaResourceEntity.Name,
            ElapsedTime = stravaResourceEntity.ElapsedTime,
            DistanceMetres = stravaResourceEntity.DistanceMetres,
            TotalElevationGain = stravaResourceEntity.TotalElevationGain,
            AverageCadence = stravaResourceEntity.AverageCadence ?? 0.0,
            Type = stravaResourceEntity.Type,
            StartDate = stravaResourceEntity.StartDate,
            AchievementCount = stravaResourceEntity.AchievementCount,
            KudosCount = stravaResourceEntity.KudosCount,
            AthleteCount = stravaResourceEntity.AthleteCount,
            PersonalRecordCount = stravaResourceEntity.PersonalRecordCount,
            ElevationHigh = stravaResourceEntity.ElevationHigh,
            ElevationLow = stravaResourceEntity.ElevationLow,
            Geomap = geomap
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

    public GeomapDto MapMapResourceDto(StravaGeoMapEntity entity)
    {
        if (entity == null) return null;
        
        return new GeomapDto()
        {
            MapId = entity.MapId,
            MapPolyline = entity.MapPolyline,
        };
    }
}