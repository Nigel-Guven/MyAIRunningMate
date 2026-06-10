using MyAIRunningMate.Application.Models.ViewObjects;
using MyAIRunningMate.Contracts.Ingestion.Responses;

namespace MyAIRunningMate.Service.Mappers;

public static class IngestionDtoExtensions
{
    public static IngestionViewResponse ToResponse(this IngestionView model) =>
        new(
            GarminActivityId: model.GarminActivityId,
            StartTime: model.StartTime,
            ExerciseType: model.ExerciseType,
            DurationSeconds: model.DurationSeconds,
            DistanceMetres: model.DistanceMetres,
            TrainingEffect: model.TrainingEffect
        );
}