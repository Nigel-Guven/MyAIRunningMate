namespace MyAIRunningMate.Contracts.Analytics.Responses;

public record AnalyticsCombinedResponse(
    YearlyAnalyticsResponse YearlyAnalytics,
    YearlyStatisticsResponse YearlyStatistics
    );