using MyAIRunningMate.Contracts.BestEfforts.Responses;

namespace MyAIRunningMate.Contracts.Aggregates.Responses;

public record AggregateArtifactResponse(
    ActivityDetailsResponse ActivityDetails,
    ActivityMetricsResponse? ActivityMetrics,
    IEnumerable<TimeSeriesRecordResponse>? TimeSeriesRecords,
    IEnumerable<LapViewResponse> Laps,
    IEnumerable<BestEffortResponse>? BestEfforts
);