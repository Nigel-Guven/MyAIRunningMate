using MyAIRunningMate.Application.Models.ViewObjects;
using MyAIRunningMate.Contracts.Views;

namespace MyAIRunningMate.Service.ViewMappers;

public static class IngestionViewDtoMapper
{
    public static IngestionViewDto ToIngestionViewDto(this IngestionView activity) => new()
    {
        GarminActivityId = activity.GarminActivityId,
        StartTime = activity.StartTime,
        ExerciseType = activity.ExerciseType,
        DurationSeconds = activity.DurationSeconds,
        DistanceMetres = activity.DistanceMetres,
        TrainingEffect = activity.TrainingEffect,
    };
}