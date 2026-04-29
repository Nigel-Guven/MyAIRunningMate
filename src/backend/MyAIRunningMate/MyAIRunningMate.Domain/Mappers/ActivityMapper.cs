using MyAIRunningMate.Domain.Entities;
using MyAIRunningMate.Domain.Models.DTO;
using MyAIRunningMate.Domain.Providers.PythonFitApi.Responses;

namespace MyAIRunningMate.Domain.Mappers;

public static class ActivityMapper
{
    public static ActivityEntity ToEntity(this ActivityDto dto) => new()
    {
        ActivityId = dto.ActivityId,
        GarminActivityId = dto.GarminActivityId,
        StartTime = dto.StartTime,
        ExerciseType = dto.ExerciseType,
        DurationSeconds = dto.DurationSeconds,
        DistanceMetres = dto.DistanceMetres,
        AverageHeartRate = dto.AverageHeartRate,
        MaxHeartRate = dto.MaxHeartRate,
        TotalElevationGain = dto.TotalElevationGain,
        AverageSecondPerKilometre = dto.AverageSecondPerKilometre,
        TrainingEffect = dto.TrainingEffect,
        StravaResourceId = dto.StravaResourceId,
    };
    
    public static ActivityDto ToDto(this ActivityEntity entity, IEnumerable<LapEntity>? lapEntities = null) => new()
    {
        ActivityId = entity.ActivityId,
        GarminActivityId = entity.GarminActivityId,
        StartTime = entity.StartTime,
        ExerciseType = entity.ExerciseType,
        DurationSeconds = entity.DurationSeconds,
        DistanceMetres = entity.DistanceMetres ?? 0.0,
        AverageHeartRate = entity.AverageHeartRate,
        MaxHeartRate = entity.MaxHeartRate,
        TotalElevationGain = entity.TotalElevationGain,
        AverageSecondPerKilometre = entity.AverageSecondPerKilometre ?? 0.0,
        TrainingEffect = entity.TrainingEffect ?? 0.0,
        StravaResourceId = entity.StravaResourceId,
        Laps = lapEntities?.Select(l => l.ToDto()) ?? Enumerable.Empty<LapDto>()
    };
    
    public static ActivityDto ToDto(this PythonAPIActivityResponse response) => new()
    {
        GarminActivityId = response.GarminId,
        StartTime = response.StartTime,
        ExerciseType = response.Type,
        DurationSeconds = response.DurationSeconds,
        DistanceMetres = response.DistanceMetres,
        AverageHeartRate = response.AverageHeartRate,
        MaxHeartRate = response.MaxHeartRate,
        TotalElevationGain = response.TotalElevationGain,
        TrainingEffect = response.TrainingEffect,
        AverageSecondPerKilometre = response.AverageSecondPerKilometre,
        Laps = response.Laps.Select(rl => rl.ToDto()),
    };
}