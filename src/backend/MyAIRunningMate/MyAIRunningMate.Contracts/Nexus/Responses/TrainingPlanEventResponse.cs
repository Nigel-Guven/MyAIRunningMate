namespace MyAIRunningMate.Contracts.Nexus.Responses;

public record TrainingPlanEventResponse(
    Guid TrainingPlanEventId,
    DateTime EventDate,
    string ExerciseType,
    string ExerciseSubtype,
    string Description,
    int DistanceMetres
);