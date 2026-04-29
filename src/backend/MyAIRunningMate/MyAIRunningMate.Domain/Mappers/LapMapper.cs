using MyAIRunningMate.Domain.Entities;
using MyAIRunningMate.Domain.Models.DTO;
using MyAIRunningMate.Domain.Providers.PythonFitApi.Responses;

namespace MyAIRunningMate.Domain.Mappers;

public static class LapMapper
{
    public static LapDto ToDto(this LapEntity entity) => new()
    {
        LapId = entity.LapId,
        LapNumber = entity.LapNumber,
        Distance = entity.DistanceMetres,
        Duration = entity.DurationSeconds,
        AverageHeartRate = entity.AverageHeartRate,
    };

    public static LapEntity ToEntity(this LapDto dto) => new()
    {
        LapId = dto.LapId,
        LapNumber = dto.LapNumber,
        DistanceMetres = dto.Distance,
        DurationSeconds = dto.Duration,
        AverageHeartRate = dto.AverageHeartRate,
    };
    
    public static LapDto ToDto(this PythonAPILap response) => new()
    {
        LapNumber = response.LapNumber,
        Distance = response.Distance,
        Duration = response.Duration,
        AverageHeartRate = response.AverageHeartRate,
    };
}