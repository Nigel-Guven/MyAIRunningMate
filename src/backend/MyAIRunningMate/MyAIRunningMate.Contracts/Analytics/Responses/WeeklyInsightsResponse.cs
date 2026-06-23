namespace MyAIRunningMate.Contracts.Analytics.Responses;

public record WeeklyInsightsResponse(
    double RunningTimeSeconds,
    double RunningMovingTimeSeconds,
    double RunningDistanceMetres,
    double TotalRunningElevationGain,
    double SwimmingTimeSeconds,
    double SwimmingDistanceMetres,
    int TotalCaloriesBurned,
    int MeanAverageHeartRate,
    int MeanMaxHeartRate,
    double TotalTrainingEffect,
    double MeanTrainingEffect,
    double TrainingConsistencyScore,
    int MorningActivities,
    int AfternoonActivities,
    int EveningActivities,
    int NightActivities,
    IEnumerable<string> Locations,
    int RestDays,
    double RunningTimeBreakSeconds,
    double ElevationIntensity,
    double CaloricIntensity,
    double RunningMovingEfficiency
);