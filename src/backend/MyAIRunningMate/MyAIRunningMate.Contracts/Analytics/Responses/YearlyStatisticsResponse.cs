namespace MyAIRunningMate.Contracts.Analytics.Responses;

public record YearlyStatisticsResponse(
    double YearlyRunningDistance,
    double YearlySwimmingDistance,
    double YearlyTimeSpentActive,
    int YearlyActiveDays,
    double YearlyBodyBatteriesUsed
);