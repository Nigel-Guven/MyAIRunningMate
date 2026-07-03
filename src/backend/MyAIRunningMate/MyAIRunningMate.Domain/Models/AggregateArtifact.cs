namespace MyAIRunningMate.Domain.Models;

public record AggregateArtifact(
    Activity GarminActivity,
    ActivityMetrics GarminActivityMetrics,
    IEnumerable<Lap> Laps,
    IEnumerable<TimeSeriesRecord>? TimeSeriesRecords,
    IEnumerable<BestEffort>? BestEfforts);