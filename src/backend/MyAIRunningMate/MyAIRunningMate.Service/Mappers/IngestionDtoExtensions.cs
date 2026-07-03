using MyAIRunningMate.Contracts.Ingestion.Responses;
using MyAIRunningMate.Domain.Models;

namespace MyAIRunningMate.Service.Mappers;

public static class IngestionDtoExtensions
{
    public static IngestionViewResponse ToResponse(this Activity model, string status) =>
        new(
            GarminActivityId: model.GarminActivityId,
            StartTime: model.StartTime,
            ExerciseType: model.ExerciseType,
            DistanceMetres: model.DistanceMetres,
            DurationSeconds: model.TotalTime,
            RecoveryTime: model.RecoveryTime,
            NumberOfLaps: model.NumberOfLaps,
            ActivityStatus: status
        );
}