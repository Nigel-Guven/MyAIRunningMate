namespace MyAIRunningMate.Contracts.Analytics.Responses;

public record YearlyAnalyticsResponse(
    YearlyStatisticsResponse Summary,
    IEnumerable<WeeklyInsightsResponse> WeeklyVolumes
);