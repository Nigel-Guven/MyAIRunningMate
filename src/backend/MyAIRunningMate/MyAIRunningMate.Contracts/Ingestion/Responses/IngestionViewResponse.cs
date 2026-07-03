namespace MyAIRunningMate.Contracts.Ingestion.Responses;

public record IngestionViewResponse(
    string GarminActivityId,
    DateTime StartTime,
    string ExerciseType,
    double DistanceMetres,
    double DurationSeconds,
    double RecoveryTime,
    int NumberOfLaps,
    string ActivityStatus
);