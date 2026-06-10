namespace MyAIRunningMate.Contracts.Analytics.Responses;

public record YearlyStatisticsResponse(
    int YearlyRunningDistance,
    int YearlySwimmingDistance,
    int YearlyActiveDays,
    double? YearlyAverageTrainingEffect,
    double? YearlyTotalTrainingEffect
);