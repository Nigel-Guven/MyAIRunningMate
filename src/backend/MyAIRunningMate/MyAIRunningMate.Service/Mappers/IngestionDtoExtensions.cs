using MyAIRunningMate.Contracts.Ingestion.Responses;
using MyAIRunningMate.Domain.Models;

namespace MyAIRunningMate.Service.Mappers;

public static class IngestionDtoExtensions
{
    public static IngestionViewResponse ToResponse(this Activity model, int numberOfLaps, string status) =>
        new(
            GarminActivityId: model.GarminActivityId,
            StartTime: model.StartTime,
            ExerciseType: model.ExerciseType,
            DurationSeconds: model.DurationSeconds,
            DistanceMetres: model.DistanceMetres,
            TrainingEffect: model.TrainingEffect,
            NumberOfLaps: numberOfLaps,
            ActivityStatus: status
        );
}