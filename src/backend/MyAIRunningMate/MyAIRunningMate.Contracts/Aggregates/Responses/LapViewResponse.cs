namespace MyAIRunningMate.Contracts.Aggregates.Responses;

public record LapViewResponse(
    int LapNumber,
    double DistanceMetres,
    double DurationSeconds,
    int AverageHeartRate,
    double AverageSpeed,
    int AverageCadence,
    string? PrimaryStroke,
    int? AverageSwolf
);