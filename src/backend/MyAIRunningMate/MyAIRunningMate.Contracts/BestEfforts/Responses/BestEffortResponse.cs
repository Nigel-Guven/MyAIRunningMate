namespace MyAIRunningMate.Contracts.BestEfforts.Responses;

public record BestEffortResponse
(
    string ExerciseType,
    double DistanceMetres,
    string DistanceLabel,
    double? TimeAchievement,
    bool IsPersonalRecord
);