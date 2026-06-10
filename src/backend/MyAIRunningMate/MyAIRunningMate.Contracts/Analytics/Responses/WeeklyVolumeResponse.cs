namespace MyAIRunningMate.Contracts.Analytics.Responses;

public record WeeklyVolumeResponse(
    int AverageMaxHeartRate,
    int AverageTrainingEffectThisWeek,
    int RunningTimeThisWeek,
    int RunningDistanceLastWeek,
    int RunningDistanceThisWeek,
    int PlannedRunningDistanceLastWeek,
    int PlannedRunningDistanceThisWeek,
    int TotalElevationGainThisWeek,
    int SwimmingDistanceThisWeek
);