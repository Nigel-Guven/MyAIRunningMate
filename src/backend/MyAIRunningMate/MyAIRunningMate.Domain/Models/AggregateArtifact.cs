namespace MyAIRunningMate.Domain.Models;

public record AggregateArtifact(
    Activity GarminActivity,
    IEnumerable<Lap> Laps,
    IEnumerable<TimeSeriesRecord> TimeSeriesRecords);