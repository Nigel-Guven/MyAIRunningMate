namespace MyAIRunningMate.Contracts.Analytics.Responses;

public record YearlyAnalyticsResponse(
    IEnumerable<double> OxygenMaxTrends,
    IEnumerable<double> LactateThresholdHeartRateTrends,
    IEnumerable<double> LactateThresholdPowerTrends,
    IEnumerable<double> LactateThresholdSpeedTrends,
    IEnumerable<double> LactateThresholdPercentageRates
);