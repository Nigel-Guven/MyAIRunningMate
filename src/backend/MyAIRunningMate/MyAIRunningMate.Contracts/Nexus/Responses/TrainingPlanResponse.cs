namespace MyAIRunningMate.Contracts.Nexus.Responses;

public record TrainingPlanResponse(
    Guid TrainingPlanId,
    string Title,
    string Description,
    DateTime StartDate,
    DateTime EndDate
);