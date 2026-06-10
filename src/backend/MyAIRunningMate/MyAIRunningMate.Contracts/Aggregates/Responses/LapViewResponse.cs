namespace MyAIRunningMate.Contracts.Aggregates.Responses;

public record LapViewResponse(
    int LapNumber,
    double DistanceMetres,
    double DurationSeconds,
    int AverageHeartRate
);