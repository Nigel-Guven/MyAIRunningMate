using MyAIRunningMate.Application.Models.ViewObjects;
using MyAIRunningMate.Contracts.Views;

namespace MyAIRunningMate.Service.ViewMappers;

public static class LapViewDtoMapper
{
    public static LapViewDto ToLapViewDto(this LapView entity) => new()
    {
        LapNumber = entity.LapNumber,
        DurationSeconds = entity.DurationSeconds,
        DistanceMetres = entity.DistanceMetres,
        AverageHeartRate = entity.AverageHeartRate,
    };
}