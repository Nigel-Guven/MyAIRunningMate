namespace MyAIRunningMate.Contracts.Aggregates.Responses;

public record AggregateArtifactResponse(
    Guid? ActivityId,
    Guid? ResourceId,
    string GarminActivityId,
    string? StravaId,
    string Name,
    string ExerciseType,
    DateTime StartTime,
    long? ElapsedTime,
    double? AverageCadence,
    double AverageSecondPerKilometre,
    double? TotalElevationGain,
    double? ElevationLow,
    double? ElevationHigh,
    double DurationSeconds,
    double DistanceMetres,
    int? AverageHeartRate,
    int? MaxHeartRate,
    double TrainingEffect,
    long? AchievementCount,
    long? KudosCount,
    long? AthleteCount,
    long? PersonalRecordCount,
    StravaGeomapViewDto? Map,
    IEnumerable<LapViewResponse> Laps
);