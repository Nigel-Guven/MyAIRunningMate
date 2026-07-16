namespace MyAIRunningMate.Contracts.Aggregates.Responses;

public record ActivityDetailsResponse(
    Guid ActivityId,
    string GarminActivityId,
    DateTime StartTime,
    double TotalTime,
    double MovingTime,
    double DistanceMetres,
    int BeginningBodyBattery,
    int BeginningBodyPotential,
    int EndingBodyBattery,
    int EndingPotential,
    int? TotalAscent,
    int? TotalDescent,
    int RecoveryTime,
    string ExerciseType,
    string ExerciseSubType,
    string ExerciseName,
    double UserVolumetricOxygenMax,
    int UserMaxHeartRate,
    int UserLactateThresholdHeartRate,
    int UserLactateThresholdPower,
    double UserLactateThresholdSpeed,
    int NumberOfLaps,
    string? Location,
    string? MapPolyline
    );