namespace MyAIRunningMate.Domain.Models;

public record Lap
{
    public Guid LapId { get; init; }
    public Guid ActivityId { get; init; }
    public int LapNumber { get; init; }
    public double DistanceMetres { get; init; }
    public double DurationSeconds { get; init; }
    public int AverageHeartRate { get; init; }

    public Lap(
        Guid lapId, 
        Guid activityId, 
        int lapNumber, 
        double distanceMetres, 
        double durationSeconds, 
        int averageHeartRate)
    {
        if (lapNumber < 0)
            throw new ArgumentException("Lap number cannot be negative.", nameof(lapNumber));

        if (distanceMetres <= 0)
            throw new ArgumentException("Lap distance must be greater than zero.", nameof(distanceMetres));

        if (durationSeconds <= 0)
            throw new ArgumentException("Lap duration must be greater than zero.", nameof(durationSeconds));

        if (averageHeartRate < 0)
            throw new ArgumentException("Average heart rate cannot be negative.", nameof(averageHeartRate));

        LapId = lapId;
        ActivityId = activityId;
        LapNumber = lapNumber;
        DistanceMetres = distanceMetres;
        DurationSeconds = durationSeconds;
        AverageHeartRate = averageHeartRate;
    }
}