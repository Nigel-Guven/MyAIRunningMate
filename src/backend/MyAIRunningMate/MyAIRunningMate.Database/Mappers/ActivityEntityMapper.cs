using System.Text.Json;
using MyAIRunningMate.Database.Entities;
using MyAIRunningMate.Domain.Models;

namespace MyAIRunningMate.Database.Mappers;

public static class ActivityEntityMapper
{
    public static Activity ToDomain(this ActivityEntity entity)
    {
        var records = !string.IsNullOrWhiteSpace(entity.TimeSeriesRecordsJson)
            ? JsonSerializer.Deserialize<List<TimeSeriesRecord>>(entity.TimeSeriesRecordsJson)
            : null;
        
        return new Activity(
            activityId: entity.ActivityId,
            userId: entity.UserId,
            garminActivityId: entity.GarminActivityId,
            startTime: entity.StartTime,
            exerciseType: entity.ExerciseType,
            durationSeconds: entity.DurationSeconds,
            movingTimeSeconds: entity.MovingTimeSeconds,
            distanceMetres: entity.DistanceMetres,
            calories: entity.Calories,
            averageHeartRate: entity.AverageHeartRate,
            maxHeartRate: entity.MaxHeartRate,
            totalElevationGain: entity.TotalElevationGain,
            trainingEffect: entity.TrainingEffect,
            rawPaceSecondsPerMetre: entity.RawPaceSecondsPerMetre,
            poolLength: entity.PoolLength,
            mapPolyline: entity.MapPolyline,
            timeSeriesRecords: records
        );
    }

    public static ActivityEntity ToEntity(this Activity domain, Guid userId) =>
        new()
        {
            ActivityId = domain.ActivityId,
            UserId = userId,
            GarminActivityId = domain.GarminActivityId,
            StartTime = domain.StartTime,
            ExerciseType = domain.ExerciseType,
            DurationSeconds = domain.DurationSeconds,
            DistanceMetres = domain.DistanceMetres,
            AverageHeartRate = domain.AverageHeartRate,
            MaxHeartRate = domain.MaxHeartRate,
            TotalElevationGain = domain.TotalElevationGain,
            RawPaceSecondsPerMetre = domain.RawPaceSecondsPerMetre,
            TrainingEffect = domain.TrainingEffect,
            TimeSeriesRecordsJson =  JsonSerializer.Serialize(domain.TimeSeriesRecords),
            MapPolyline = domain.MapPolyline ?? null
        };
}