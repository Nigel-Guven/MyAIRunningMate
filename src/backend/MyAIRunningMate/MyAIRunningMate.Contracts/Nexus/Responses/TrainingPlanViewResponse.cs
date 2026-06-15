namespace MyAIRunningMate.Contracts.Nexus.Responses;

public record TrainingPlanViewResponse(
    TrainingPlanResponse TrainingPlan,
    IEnumerable<TrainingPlanEventResponse> Events
);