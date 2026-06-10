using MyAIRunningMate.Application.Models.ViewObjects;
using MyAIRunningMate.Contracts.Views;

namespace MyAIRunningMate.Service.Mappers;

public static class LapViewDtoMapper
{
    public static LapViewDto ToLapViewDto(this LapView model) => new()
    {
        LapNumber = model.LapNumber,
        DurationSeconds = model.DurationSeconds,
        DistanceMetres = model.DistanceMetres,
        AverageHeartRate = model.AverageHeartRate,
    };
}