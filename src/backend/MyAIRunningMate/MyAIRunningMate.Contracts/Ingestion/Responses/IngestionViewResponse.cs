namespace MyAIRunningMate.Contracts.Ingestion.Responses;

public record IngestionViewResponse(
    string GarminActivityId,
    DateTime StartTime,
    string ExerciseType,
    int EndingBodyBattery,
    int EndingPotential,
    double RecoveryTime,
    int NumberOfLaps,
    string ActivityStatus
);