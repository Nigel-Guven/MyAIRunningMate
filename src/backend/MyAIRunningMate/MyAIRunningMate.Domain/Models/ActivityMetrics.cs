namespace MyAIRunningMate.Domain.Models;

public class ActivityMetrics
{
    public Guid ActivityId { get; init; }
    public double ElapsedTime { get; init; }
    public double MovingTime { get; init; }
    public double DistanceMetres { get; init; }
    public int TotalCycles { get; init; }
    public int TotalCalories { get; init; }
    public int? EstimatedSweatLoss { get; init; }
    public int? AverageTemperature { get; init; }
    public int? MaxTemperature { get; init; }
    public int AverageHeartRate { get; init; }
    public int MaxHeartRate { get; init; }
    public int? AveragePower { get; init; }
    public int? MaxPower { get; init; }
    public int AverageCadence { get; init; }
    public int? MaxCadence { get; init; }
    public double? AverageVerticalOscillation { get; init; }
    public double? StepLength { get; init; }
    public double? AverageVerticalRatio { get; init; }
    public double? AverageStanceTime { get; init; }
    public double AerobicTrainingEffect { get; init; }
    public double AnaerobicTrainingEffect { get; init; }
    public int? AverageSwolf { get; init; }
    public int? PoolLength { get; init; }

    public ActivityMetrics(
        Guid activityId,
        double elapsedSeconds,
        double movingTime,
        double distanceMetres,
        int totalCycles,
        int totalCalories,
        int averageHeartRate,
        int maxHeartRate,
        int averageCadence,
        double aerobicTrainingEffect,
        double anaerobicTrainingEffect,
        int? estimatedSweatLoss = null,
        int? averageTemperature = null,
        int? maxTemperature = null,
        int? averagePower = null,
        int? maxPower = null,
        int? maxCadence = null,
        double? averageVerticalOscillation = null,
        double? stepLength = null,
        double? averageVerticalRatio = null,
        double? averageStanceTime = null,
        int? averageSwolf = null,
        int? poolLength = null)
    {
        ActivityId = activityId;
        ElapsedTime = elapsedSeconds;
        MovingTime = movingTime;
        DistanceMetres = distanceMetres;
        TotalCycles = totalCycles;
        TotalCalories = totalCalories;
        AverageHeartRate = averageHeartRate;
        MaxHeartRate = maxHeartRate;
        AverageCadence = averageCadence;
        AerobicTrainingEffect = aerobicTrainingEffect;
        AnaerobicTrainingEffect = anaerobicTrainingEffect;
        EstimatedSweatLoss = estimatedSweatLoss;
        AverageTemperature = averageTemperature;
        MaxTemperature = maxTemperature;
        AveragePower = averagePower;
        MaxPower = maxPower;
        MaxCadence = maxCadence;
        AverageVerticalOscillation = averageVerticalOscillation;
        StepLength = stepLength;
        AverageVerticalRatio = averageVerticalRatio;
        AverageStanceTime = averageStanceTime;
        AverageSwolf = averageSwolf;
        PoolLength = poolLength;
    }
}