using MyAIRunningMate.Application.Models;
using MyAIRunningMate.Database.Entities;

namespace MyAIRunningMate.Database.Mappings;

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