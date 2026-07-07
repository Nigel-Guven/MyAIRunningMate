namespace MyAIRunningMate.Domain.Models;

public record Lap
{
    public Guid LapId { get; init; }
    public Guid ActivityId { get; init; }
    public int LapNumber { get; init; }
    public DateTime LapStartTime { get; init; }
    public double DistanceMetres { get; init; }
    public double DurationSeconds { get; init; }
    public int AverageHeartRate { get; init; }
    public int MaxHeartRate { get; init; }
    public double AverageSpeed { get; init; }
    public int? AverageCadence { get; init; }
    public string? PrimaryStroke { get; init; }
    public int? NumberOfLengths { get; init; }

    public Lap(
        Guid lapId, 
        Guid activityId, 
        int lapNumber, 
        DateTime lapStartTime,
        double distanceMetres, 
        double durationSeconds, 
        int averageHeartRate,
        int maxHeartRate,
        double averageSpeed,
        int? averageCadence, 
        string? primaryStroke,
        int? numberOfLengths)
    {
        if (lapNumber < 0)
            throw new ArgumentException("Lap number cannot be negative.", nameof(lapNumber));

        if (distanceMetres < 0) // FIX: Changed from <= 0 to allow kickboard/drill lap updates
            throw new ArgumentException("Lap distance cannot be negative.", nameof(distanceMetres));

        if (durationSeconds <= 0)
            throw new ArgumentException("Lap duration must be greater than zero.", nameof(durationSeconds));

        if (averageHeartRate < 0)
            throw new ArgumentException("Average heart rate cannot be negative.", nameof(averageHeartRate));

        LapId = lapId;
        ActivityId = activityId;
        LapNumber = lapNumber;
        LapStartTime = lapStartTime;
        DistanceMetres = distanceMetres;
        DurationSeconds = durationSeconds;
        AverageHeartRate = averageHeartRate;
        MaxHeartRate = maxHeartRate;
        AverageSpeed = averageSpeed;
        AverageCadence = averageCadence;
        PrimaryStroke = primaryStroke;
        NumberOfLengths = numberOfLengths;
    }
}