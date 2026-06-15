namespace MyAIRunningMate.Domain.Models;

public record TrainingPlanFinalizeResult(
    Guid TrainingPlanId,
    string Message,
    int EventsSaved
);