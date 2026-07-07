namespace MyAIRunningMate.Contracts.BestEfforts.Responses;

public record BestEffortResponse
(
    Guid ActivityLinkedId,
    string ExerciseType,
    double DistanceMetres,
    string DistanceLabel,
    double? TimeAchievement
);