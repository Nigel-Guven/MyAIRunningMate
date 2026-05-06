using MyAIRunningMate.Domain.DatabaseEntities;

namespace MyAIRunningMate.Application.Models.ViewObjects;

public static class LapViewMapper
{
    public static LapView ToLapView(this LapEntity entity) => new()
    {
        LapNumber = entity.LapNumber,
        DurationSeconds = entity.DurationSeconds,
        DistanceMetres = entity.DistanceMetres,
        AverageHeartRate = entity.AverageHeartRate,
    };
}