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
            averageHeartRate: entity.AverageHeartRate
        );

    public static LapEntity ToEntity(this Lap domain) =>
        new()
        {
            LapId = domain.LapId,
            ActivityId = domain.ActivityId,
            LapNumber = domain.LapNumber,
            DistanceMetres = domain.DistanceMetres,
            DurationSeconds = domain.DurationSeconds,
            AverageHeartRate = domain.AverageHeartRate
        };
}