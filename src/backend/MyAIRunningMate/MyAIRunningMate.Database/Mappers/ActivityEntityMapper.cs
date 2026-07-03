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
            elapsedSeconds: entity.TotalTime,
            movingTime: entity.MovingTime,
            distanceMetres: entity.DistanceMetres,
            beginningBodyBattery: entity.BeginningBodyBattery,
            beginningBodyPotential: entity.BeginningBodyPotential,
            endingBodyBattery: entity.EndingBodyBattery,
            endingPotential: entity.EndingPotential,
            totalAscent: entity.TotalAscent,
            totalDescent: entity.TotalDescent,
            recoveryTime: entity.RecoveryTime,
            exerciseType: entity.ExerciseType,
            exerciseSubType: entity.ExerciseSubType,
            exerciseName: entity.ExerciseName,
            userVolumetricOxygenMax: entity.UserVolumetricOxygenMax,
            userMaxHeartRate: entity.UserMaxHeartRate,
            userLactateThresholdHeartRate: entity.UserLactateThresholdHeartRate,
            userLactateThresholdPower: entity.UserLactateThresholdPower,
            userLactateThresholdSpeed: entity.UserLactateThresholdSpeed,
            numberOfLaps: entity.NumberOfLaps,
            location: entity.Location,
            mapPolyline: entity.MapPolyline
        );

    public static ActivityEntity ToEntity(this Activity domain) =>
        new()
        {
            ActivityId = domain.ActivityId,
            UserId = domain.UserId,
            GarminActivityId = domain.GarminActivityId,
            StartTime = domain.StartTime,
            TotalTime = domain.TotalTime,
            MovingTime = domain.MovingTime,
            DistanceMetres = domain.DistanceMetres,
            ExerciseType = domain.ExerciseType,
            ExerciseSubType = domain.ExerciseSubType,
            ExerciseName = domain.ExerciseName,
            BeginningBodyBattery = domain.BeginningBodyBattery,
            BeginningBodyPotential = domain.BeginningBodyPotential,
            EndingBodyBattery = domain.EndingBodyBattery,
            EndingPotential = domain.EndingPotential,
            TotalAscent = domain.TotalAscent,
            TotalDescent = domain.TotalDescent,
            RecoveryTime = domain.RecoveryTime,
            UserVolumetricOxygenMax = domain.UserVolumetricOxygenMax,
            UserMaxHeartRate = domain.UserMaxHeartRate,
            UserLactateThresholdHeartRate = domain.UserLactateThresholdHeartRate,
            UserLactateThresholdPower = domain.UserLactateThresholdPower,
            UserLactateThresholdSpeed = domain.UserLactateThresholdSpeed,
            NumberOfLaps = domain.NumberOfLaps,
            Location = domain.Location,
            MapPolyline = domain.MapPolyline ?? null
        };
}