using MyAIRunningMate.Database.Entities;
using MyAIRunningMate.Domain.Models;

namespace MyAIRunningMate.Database.Mappers;

public static class ActivityEntityMapper
{
    public static Activity ToDomain(this ActivityEntity entity) =>
        new(
            activityId: entity.ActivityId,
            userId: entity.UserId,
            garminActivityId: entity.GarminActivityId,
            startTime: entity.StartTime,
            exerciseType: entity.ExerciseType,
            durationSeconds: entity.DurationSeconds,
            distanceMetres: entity.DistanceMetres,
            averageHeartRate: entity.AverageHeartRate,
            maxHeartRate: entity.MaxHeartRate,
            totalElevationGain: entity.TotalElevationGain,
            averageSecondPerKilometre: entity.AverageSecondPerKilometre,
            trainingEffect: entity.TrainingEffect,
            stravaResourceId: entity.StravaResourceId
        );

    public static ActivityEntity ToEntity(this Activity domain) =>
        new()
        {
            ActivityId = domain.ActivityId,
            UserId = domain.UserId,
            GarminActivityId = domain.GarminActivityId,
            StartTime = domain.StartTime,
            ExerciseType = domain.ExerciseType,
            DurationSeconds = domain.DurationSeconds,
            DistanceMetres = domain.DistanceMetres,
            AverageHeartRate = domain.AverageHeartRate,
            MaxHeartRate = domain.MaxHeartRate,
            TotalElevationGain = domain.TotalElevationGain,
            AverageSecondPerKilometre = domain.AverageSecondPerKilometre,
            TrainingEffect = domain.TrainingEffect,
            StravaResourceId = domain.StravaResourceId
        };
}