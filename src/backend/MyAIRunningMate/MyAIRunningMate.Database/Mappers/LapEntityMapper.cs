using MyAIRunningMate.Database.Entities;
using MyAIRunningMate.Domain.Models;

namespace MyAIRunningMate.Database.Mappers;

public static class LapEntityMapper
{
    public static Lap ToDomain(this LapEntity entity) =>
        new(
            lapId: entity.LapId,
            activityId: entity.ActivityId,
            lapNumber: entity.LapNumber,
            distanceMetres: entity.DistanceMetres,
            durationSeconds: entity.DurationSeconds,
            averageHeartRate: entity.AverageHeartRate,
            averageSpeed: entity.AverageSpeed,
            averageCadence: entity.AverageCadence,
            primaryStroke: entity.PrimaryStroke,
            averageSwolf: entity.AverageSwolf
        );

    public static LapEntity ToEntity(this Lap domain, Guid activityId) =>
        new()
        {
            LapId = domain.LapId,
            ActivityId = activityId,
            LapNumber = domain.LapNumber,
            DistanceMetres = domain.DistanceMetres,
            DurationSeconds = domain.DurationSeconds,
            AverageHeartRate = domain.AverageHeartRate,
            AverageSpeed = domain.AverageSpeed,
            AverageCadence = domain.AverageCadence,
            PrimaryStroke = domain.PrimaryStroke,
            AverageSwolf = domain.AverageSwolf
        };
}