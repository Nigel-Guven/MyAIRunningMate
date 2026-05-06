using MyAIRunningMate.Application.Models;
using MyAIRunningMate.Domain.DatabaseEntities;

namespace MyAIRunningMate.Database.DbEntityMappings;

public static class LapEntityMapper
{
    public static LapEntity ToLapEntity(this Lap lap) => new()
    {
        LapNumber = lap.LapNumber,
        DistanceMetres = lap.Distance,
        DurationSeconds = lap.Duration,
        AverageHeartRate = lap.AverageHeartRate,
    };
}