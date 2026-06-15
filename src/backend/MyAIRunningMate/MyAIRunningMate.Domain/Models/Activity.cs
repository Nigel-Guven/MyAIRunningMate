namespace MyAIRunningMate.Domain.Models;

public record Activity
{
    public Guid ActivityId { get; init; }
    public Guid UserId { get; init; }
    public string GarminActivityId { get; init; }
    public DateTime StartTime { get; init; }
    public string ExerciseType { get; init; }
    public double DurationSeconds { get; init; }
    public double MovingTimeSeconds { get; init; }
    public double DistanceMetres { get; init; }
    public int Calories { get; init; }
    public int AverageHeartRate { get; init; }
    public int MaxHeartRate { get; init; }
    public double? TotalElevationGain { get; init; }
    public double TrainingEffect { get; init; }
    public double? RawPaceSecondsPerMetre { get; init; }
    public int? PoolLength { get; init; }
    public string? MapPolyline { get; init; }
    public IReadOnlyCollection<TimeSeriesRecord> TimeSeriesRecords { get; init; }

    public Activity(
        Guid activityId,
        Guid userId,
        string garminActivityId,
        DateTime startTime,
        string exerciseType,
        double durationSeconds,
        double movingTimeSeconds,
        double distanceMetres,
        int calories,
        int averageHeartRate,
        int maxHeartRate,
        double? totalElevationGain,
        double trainingEffect,
        double? rawPaceSecondsPerMetre,
        int? poolLength,
        string? mapPolyline,
        IEnumerable<TimeSeriesRecord>? timeSeriesRecords)
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
        GarminActivityId = garminActivityId;
        StartTime = startTime;
        ExerciseType = exerciseType;
        DurationSeconds = durationSeconds;
        MovingTimeSeconds = movingTimeSeconds;
        DistanceMetres = distanceMetres;
        Calories = calories;
        AverageHeartRate = averageHeartRate;
        MaxHeartRate = maxHeartRate;
        TotalElevationGain = totalElevationGain;
        RawPaceSecondsPerMetre = rawPaceSecondsPerMetre;
        TrainingEffect = trainingEffect;
        PoolLength = poolLength;
        MapPolyline = mapPolyline;
        TimeSeriesRecords = timeSeriesRecords != null 
            ? new List<TimeSeriesRecord>(timeSeriesRecords).AsReadOnly() 
            : Array.Empty<TimeSeriesRecord>();
    }
}