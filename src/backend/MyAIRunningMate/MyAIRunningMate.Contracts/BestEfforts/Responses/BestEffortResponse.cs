namespace MyAIRunningMate.Contracts.BestEfforts.Responses;

public record BestEffortResponse
(
    int DistanceMetres,
    string DistanceLabel,
    int? TimeAchievement,
    DateTime? AchievementDate
);