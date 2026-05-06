using MyAIRunningMate.Application.Models;
using MyAIRunningMate.Contracts.Views;

namespace MyAIRunningMate.Application.Mappers;

public static class IngestionViewMapper
{
    public static IngestionViewDto ToIngestionView(this Activity activity) => new()
    {
        GarminActivityId = activity.GarminActivityId,
        StartTime = activity.StartTime,
        ExerciseType = activity.ExerciseType,
        DurationSeconds = activity.DurationSeconds,
        DistanceMetres = activity.DistanceMetres,
        TrainingEffect = activity.TrainingEffect,
    };
}