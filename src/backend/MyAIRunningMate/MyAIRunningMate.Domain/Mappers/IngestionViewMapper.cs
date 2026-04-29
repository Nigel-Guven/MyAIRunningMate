using MyAIRunningMate.Domain.Entities;
using MyAIRunningMate.Domain.Models.DTO;

namespace MyAIRunningMate.Domain.Mappers;

public static class IngestionViewMapper
{
    public static IngestionViewDto ToIngestionView(this ActivityEntity entity) => new()
    {
        GarminActivityId = entity.GarminActivityId,
        StartTime = entity.StartTime,
        ExerciseType = entity.ExerciseType,
        DurationSeconds = entity.DurationSeconds,
        DistanceMetres = entity.DistanceMetres,
        TrainingEffect = entity.TrainingEffect,
    };
    
    public static IngestionViewDto ToIngestionView(this ActivityDto entity) => new()
    {
        GarminActivityId = entity.GarminActivityId,
        StartTime = entity.StartTime,
        ExerciseType = entity.ExerciseType,
        DurationSeconds = entity.DurationSeconds,
        DistanceMetres = entity.DistanceMetres,
        TrainingEffect = entity.TrainingEffect,
    };
}