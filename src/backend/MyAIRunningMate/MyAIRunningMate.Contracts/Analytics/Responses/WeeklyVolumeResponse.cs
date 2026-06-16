namespace MyAIRunningMate.Contracts.Analytics.Responses;

public record WeeklyVolumeResponse(
    int AverageMaxHeartRate,
    int AverageTrainingEffectThisWeek,
    int RunningTimeThisWeek,
    int RunningDistanceThisWeek,
    int PlannedRunningDistanceThisWeek,
    int TotalElevationGainThisWeek,
    int SwimmingDistanceThisWeek
);