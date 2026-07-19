namespace MyAIRunningMate.Domain.Models;

public class YearlyAnalytics
{
    public IEnumerable<double> OxygenMaxTrends { get; set; }
    public IEnumerable<double> LactateThresholdHeartRateTrends { get; set; }
    public IEnumerable<double> LactateThresholdPowerTrends { get; set; }
    public IEnumerable<double> LactateThresholdSpeedTrends { get; set; }
    public IEnumerable<double> LactateThresholdPercentageRates { get; set; }
}