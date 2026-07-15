namespace MyAIRunningMate.Contracts.Analytics.Responses;

public record UserMetricsResponse(
    double WeightKg,
    double UserVolumetricOxygenMax,
    int UserMaxHeartRate,
    int UserLactateThresholdHeartRate,
    int UserLactateThresholdPower,
    double UserLactateThresholdSpeed,
    string UserVolumetricOxygenMaxRating,
    int FitnessPercentile,
    double PowerToWeightRatio,
    string PowerRating,
    double ThresholdPercentagePower,
    string TrainingLevel,
    string FitnessRankColor
);