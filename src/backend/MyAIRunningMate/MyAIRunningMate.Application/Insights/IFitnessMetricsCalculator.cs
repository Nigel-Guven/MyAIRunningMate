namespace MyAIRunningMate.Application.Insights;

public interface IFitnessMetricsCalculator
{
    string GetVo2MaxRating(double vo2Max);
    string GetFitnessRankColor(double vo2Max);
    int GetFitnessPercentile(double vo2Max);
    double CalculatePowerToWeight(int thresholdPower, double weightKg);
    string GetPowerRating(double powerToWeight);
    double CalculateThresholdPercentageOfMaxHr(int thresholdHr, int maxHr);
    string GetTrainingLevel(double vo2Max, double powerToWeight);
}