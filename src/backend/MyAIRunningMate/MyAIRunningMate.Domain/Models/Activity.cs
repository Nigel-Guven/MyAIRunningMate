namespace MyAIRunningMate.Domain.Models;

public record Activity
{
    public Guid ActivityId { get; init; }
    public Guid UserId { get; init; }
    public string GarminActivityId { get; init; }
    public DateTime StartTime { get; init; }
    public double TotalTime { get; init; }
    public double MovingTime { get; init; }
    public double DistanceMetres { get; init; }
    public int BeginningBodyBattery { get; init; }
    public int BeginningBodyPotential { get; init; }
    public int EndingBodyBattery { get; init; }
    public int EndingPotential { get; init; }
    public int? TotalAscent { get; init; }
    public int? TotalDescent { get; init; }
    public double RecoveryTime { get; init; }
    public string ExerciseType { get; init; }
    public string ExerciseSubType { get; init; }
    public string ExerciseName { get; init; }
    public double UserVolumetricOxygenMax { get; init; }
    public int UserMaxHeartRate { get; init; }
    public double UserLactateThresholdHeartRate { get; init; }
    public double UserLactateThresholdPower { get; init; }
    public double UserLactateThresholdSpeed { get; init; }
    public int NumberOfLaps { get; init; }
    public string? Location { get; init; }
    public string? MapPolyline { get; init; }

    public Activity(
        Guid activityId,
        Guid userId,
        string garminActivityId,
        DateTime startTime,
        double elapsedSeconds,
        double movingTime,
        double distanceMetres,
        int beginningBodyBattery,
        int beginningBodyPotential,
        int endingBodyBattery,
        int endingPotential,
        double recoveryTime,
        string exerciseType,
        string exerciseSubType,
        string exerciseName,
        double userVolumetricOxygenMax,
        int userMaxHeartRate,
        double userLactateThresholdHeartRate,
        double userLactateThresholdPower,
        double userLactateThresholdSpeed,
        int numberOfLaps,
        int? totalAscent = null,
        int? totalDescent = null,
        string? location = null,
        string? mapPolyline = null)
    {
        ActivityId = activityId;
        UserId = userId;
        GarminActivityId = garminActivityId;
        StartTime = startTime;
        TotalTime = elapsedSeconds;
        MovingTime = movingTime;
        DistanceMetres = distanceMetres;
        BeginningBodyBattery = beginningBodyBattery;
        BeginningBodyPotential = beginningBodyPotential;
        EndingBodyBattery = endingBodyBattery;
        EndingPotential = endingPotential;
        TotalAscent = totalAscent;
        TotalDescent = totalDescent;
        RecoveryTime = recoveryTime;
        ExerciseType = exerciseType;
        ExerciseSubType = exerciseSubType;
        ExerciseName = exerciseName;
        UserVolumetricOxygenMax = userVolumetricOxygenMax;
        UserMaxHeartRate = userMaxHeartRate;
        UserLactateThresholdHeartRate = userLactateThresholdHeartRate;
        UserLactateThresholdPower = userLactateThresholdPower;
        UserLactateThresholdSpeed = userLactateThresholdSpeed;
        NumberOfLaps = numberOfLaps;
        Location = location;
        MapPolyline = mapPolyline;
    }
}