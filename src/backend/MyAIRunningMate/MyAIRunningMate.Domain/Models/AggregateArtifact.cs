namespace MyAIRunningMate.Domain.Models;

public record AggregateArtifact(
    Activity GarminActivity,
    IEnumerable<ActivityMetrics> GarminActivityMetrics,
    IEnumerable<Lap> Laps,
    IEnumerable<TimeSeriesRecord>? TimeSeriesRecords,
    IEnumerable<BestEffort>? BestEfforts);