using MyAIRunningMate.Application.Models.ViewObjects;
using MyAIRunningMate.Contracts.Views;

namespace MyAIRunningMate.Service.Mappers;

public static class IngestionViewDtoMapper
{
    public static IngestionViewDto ToIngestionViewDto(this IngestionView model) => new()
    {
        GarminActivityId = model.GarminActivityId,
        StartTime = model.StartTime,
        ExerciseType = model.ExerciseType,
        DurationSeconds = model.DurationSeconds,
        DistanceMetres = model.DistanceMetres,
        TrainingEffect = model.TrainingEffect,
    };
}