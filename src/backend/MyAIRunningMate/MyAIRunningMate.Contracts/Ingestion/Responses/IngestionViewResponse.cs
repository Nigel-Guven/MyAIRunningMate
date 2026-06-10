namespace MyAIRunningMate.Contracts.Ingestion.Responses;

public record IngestionViewResponse(
    string GarminActivityId,
    DateTime StartTime,
    string ExerciseType,
    double DurationSeconds,
    double DistanceMetres,
    double TrainingEffect
);