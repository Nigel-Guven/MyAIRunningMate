namespace MyAIRunningMate.Contracts.Aggregates.Responses;

public record LapViewResponse(
    int LapNumber,
    DateTime LapStartTime,
    double DurationSeconds,
    double DistanceMetres,
    double AverageSpeed,
    int AverageHeartRate,
    int MaxHeartRate,
    int? AverageCadence,
    string? PrimaryStroke,
    int? NumberOfLengths
);