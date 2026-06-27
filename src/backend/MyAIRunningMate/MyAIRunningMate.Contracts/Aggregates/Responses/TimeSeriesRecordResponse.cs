namespace MyAIRunningMate.Contracts.Aggregates.Responses;

public record TimeSeriesRecordResponse
{
    public DateTime Timestamp { get; init; }
    public double? DistanceMetres { get; init; }
    public int? HeartRate { get; init; }
    public int? Cadence { get; init; }
    public double? Latitude { get; init; }
    public double? Longitude { get; init; }
}