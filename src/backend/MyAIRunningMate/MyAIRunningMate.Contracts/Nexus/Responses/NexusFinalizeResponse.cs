namespace MyAIRunningMate.Contracts.Nexus.Responses;

public record NexusFinalizeResponse(
    Guid TrainingPlanId,
    string Message,
    int EventsSaved
);