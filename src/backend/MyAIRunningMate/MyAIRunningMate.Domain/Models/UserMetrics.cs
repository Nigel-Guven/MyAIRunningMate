namespace MyAIRunningMate.Domain.Models;

public class UserMetrics
{
    public int Age { get; init; }
    public double WeightKg { get; init; }
    public double UserVolumetricOxygenMax { get; init; }
    public int UserMaxHeartRate { get; init; }
    public int UserLactateThresholdHeartRate { get; init; }
    public int UserLactateThresholdPower { get; init; }
    public double UserLactateThresholdSpeed { get; init; }
    public string UserVolumetricOxygenMaxRating { get; init; }
    public int FitnessPercentile { get; init; }
    public double PowerToWeightRatio { get; init; }
    public string PowerRating { get; init; }
    public double ThresholdPercentagePower { get; init; }
    public string TrainingLevel { get; init; }
    public string FitnessRankColor { get; init; }
}