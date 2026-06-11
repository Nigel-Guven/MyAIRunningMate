namespace MyAIRunningMate.Domain.Models;

public record Activity
{
    public Guid ActivityId { get; init; }
    public Guid UserId { get; init; }
    public string GarminActivityId { get; init; }
    public DateTime StartTime { get; init; }
    public string ExerciseType { get; init; }
    public double DurationSeconds { get; init; }
    public double? DistanceMetres { get; init; }
    public int AverageHeartRate { get; init; }
    public int MaxHeartRate { get; init; }
    public double? TotalElevationGain { get; init; }
    public double? AverageSecondPerKilometre { get; init; }
    public double? TrainingEffect { get; init; }
    public Guid? StravaResourceId { get; init; }

    public Activity(
        Guid activityId,
        Guid userId,
        string garminActivityId,
        DateTime startTime,
        string exerciseType,
        double durationSeconds,
        double? distanceMetres,
        int averageHeartRate,
        int maxHeartRate,
        double? totalElevationGain,
        double? averageSecondPerKilometre,
        double? trainingEffect,
        Guid? stravaResourceId)
    {
        if (durationSeconds <= 0)
            throw new ArgumentException("Activity duration must be greater than zero seconds.", nameof(durationSeconds));

        if (averageHeartRate < 0 || maxHeartRate < 0)
            throw new ArgumentException("Heart rate metrics cannot be negative values.", nameof(averageHeartRate));

        if (maxHeartRate < averageHeartRate)
            throw new ArgumentException("Maximum heart rate cannot be lower than the calculated average heart rate.", nameof(maxHeartRate));

        if (distanceMetres is < 0)
            throw new ArgumentException("Tracked distance cannot be a negative value.", nameof(distanceMetres));
        
        ActivityId = activityId;
        UserId = userId;
        GarminActivityId = garminActivityId ?? string.Empty;
        StartTime = startTime;
        ExerciseType = string.IsNullOrWhiteSpace(exerciseType) ? "Running" : exerciseType;
        DurationSeconds = durationSeconds;
        DistanceMetres = distanceMetres;
        AverageHeartRate = averageHeartRate;
        MaxHeartRate = maxHeartRate;
        TotalElevationGain = totalElevationGain;
        AverageSecondPerKilometre = averageSecondPerKilometre;
        TrainingEffect = trainingEffect;
        StravaResourceId = stravaResourceId;
    }
}