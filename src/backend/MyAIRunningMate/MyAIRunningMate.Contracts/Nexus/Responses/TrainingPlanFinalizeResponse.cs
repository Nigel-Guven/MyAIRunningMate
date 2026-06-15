namespace MyAIRunningMate.Contracts.Nexus.Responses;

public record TrainingPlanFinalizeResponse(
    Guid TrainingPlanId,
    string Message,
    int EventsSaved
);