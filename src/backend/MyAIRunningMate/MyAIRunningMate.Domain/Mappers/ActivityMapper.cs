using MyAIRunningMate.Domain.Entities;
using MyAIRunningMate.Domain.Models.DTO;

namespace MyAIRunningMate.Domain.Mappers;

public static class ActivityMapper
{
    // DTO -> Entity
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

    // Entity -> DTO
    public static ActivityDto ToDto(this ActivityEntity entity, IEnumerable<LapEntity>? lapEntities = null) => new()
    {
        ActivityId = entity.ActivityId,
        GarminActivityId = entity.GarminActivityId,
        StartTime = entity.StartTime,
        ExerciseType = entity.ExerciseType,
        DurationSeconds = entity.DurationSeconds,
        DistanceMetres = entity.DistanceMetres,
        AverageHeartRate = entity.AverageHeartRate,
        MaxHeartRate = entity.MaxHeartRate,
        TotalElevationGain = entity.TotalElevationGain,
        AverageSecondPerKilometre = entity.AverageSecondPerKilometre,
        TrainingEffect = entity.TrainingEffect,
        StravaResourceId = entity.StravaResourceId,
        Laps = lapEntities?.Select(l => l.ToDto()) ?? Enumerable.Empty<LapDto>()
    };
}