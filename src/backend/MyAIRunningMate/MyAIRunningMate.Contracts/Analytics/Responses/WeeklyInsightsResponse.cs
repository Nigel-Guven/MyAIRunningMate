namespace MyAIRunningMate.Contracts.Analytics.Responses;

public record WeeklyInsightsResponse(
    double RunningTimeVolume,
    double RunningDistanceVolume,
    double SwimmingTimeVolume,
    double SwimmingDistanceVolume,
    double TotalRunningElevationGain,
    int MeanAverageHeartRate,
    int MeanMaxHeartRate,
    double TotalTrainingEffect,
    double MeanTrainingEffect,
    long TotalAchievementCount,
    long TotalPersonalRecordCount,
    long TotalPersonalExercises,
    long TotalGroupExercises,
    IEnumerable<string> Locations,
    int RestDays
);