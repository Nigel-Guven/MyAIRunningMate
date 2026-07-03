using System.Text.Json.Serialization;

namespace MyAIRunningMate.Contracts.Aggregates.Responses;

public record AggregateArtifactResponse(
    Guid? ActivityId,
    string GarminActivityId,
    DateTime StartTime,
    string ExerciseType,
    double DurationSeconds,
    double MovingTimeSeconds,
    double DistanceMetres,
    int Calories,
    int? AverageHeartRate,
    int? MaxHeartRate,
    double? TotalElevationGain,
    double TrainingEffect,
    int? PoolLength,
    string? MapPolyline,
    IEnumerable<TimeSeriesRecordResponse>? TimeSeriesRecords,
    IEnumerable<LapViewResponse> Laps
);