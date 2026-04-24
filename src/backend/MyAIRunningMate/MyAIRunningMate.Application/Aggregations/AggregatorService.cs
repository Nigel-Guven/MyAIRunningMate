using MyAIRunningMate.Domain.Activities;
using MyAIRunningMate.Domain.Entities;
using MyAIRunningMate.Domain.Interfaces.Services;
using MyAIRunningMate.Domain.Models;
using MyAIRunningMate.Domain.Models.Activities;
using MyAIRunningMate.Domain.Models.Platform;

namespace MyAIRunningMate.Application.Aggregations;

public class AggregatorService : IActivityAggregatorService
{
    public AggregateArtifactDto CreateAggregateArtifactDto(ActivityDto garminActivityDto,
        StravaActivityDto stravaActivityDto)
    {
        throw new NotImplementedException();
    }

    public StravaResourceDto MapStravaArtifactDto(StravaResourceEntity stravaResourceEntity)
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
            MapId = stravaResourceEntity.MapId,
            CreatedAt = stravaResourceEntity.CreatedAt,
        };
    }

    public ActivityDto MapGarminActivityDto(ActivityEntity entity)
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
            StravaResourceId = entity.StravaResourceId,
            CreatedAt = entity.CreatedAt,
        };
    }

    public LapDto MapLapDto(LapEntity entity)
    {
        return new LapDto()
        {
            LapId = entity.LapId,
            LapNumber =  entity.LapNumber,
            
        };
    }

    public MapDto MapMapResourceDto(StravaResourceMapEntity entity)
    {
        throw new NotImplementedException();
    }
}