namespace MyAIRunningMate.Contracts.Aggregates.Responses;

public record TimeSeriesRecordResponse(
    DateTime Timestamp,
    double? DistanceMetres,
    int? HeartRate,
    int? Cadence,
    double? Latitude,
    double? Longitude
);