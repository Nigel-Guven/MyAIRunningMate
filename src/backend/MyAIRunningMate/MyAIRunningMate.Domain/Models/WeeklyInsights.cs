namespace MyAIRunningMate.Domain.Models;

public class WeeklyInsights
{
    /* --- Running Metrics --- */
    public double RunningTimeSeconds { get; init; }
    public double RunningMovingTimeSeconds { get; init; }
    public double RunningDistanceMetres { get; init; }
    public double TotalRunningElevationGain { get; init; }
    
    /* --- Swimming Metrics --- */
    public double SwimmingTimeSeconds { get; init; }
    public double SwimmingDistanceMetres { get; init; }
    
    /* --- Health & Training Scores --- */
    public int TotalCaloriesBurned { get; init; }
    public int MeanAverageHeartRate { get; init; }
    public int MeanMaxHeartRate { get; init; }
    public double TotalTrainingEffect { get; init; }
    public double MeanTrainingEffect { get; init; }
    
    /* --- Activity Distribution --- */
    public int MorningActivities { get; init; }
    public int AfternoonActivities { get; init; }
    public int EveningActivities { get; init; }
    public int NightActivities { get; init; }
    
    /* --- Meta --- */
    public IEnumerable<string> Locations { get; init; } = Enumerable.Empty<string>();
    public int RestDays { get; init; }

    /* --- Derived Insights --- */
    
    //Running Break Time
    public double RunningTimeBreakSeconds => RunningTimeSeconds - RunningMovingTimeSeconds; 
    
    // Elevation Gain per km (Standard measure of route difficulty)
    public double ElevationIntensity => 
        RunningDistanceMetres > 0 ? TotalRunningElevationGain / (RunningDistanceMetres / 1000.0) : 0;

    // Calories per km (Metabolic efficiency)
    public double CaloricIntensity => 
        (RunningDistanceMetres + SwimmingDistanceMetres) > 0 
            ? TotalCaloriesBurned / ((RunningDistanceMetres + SwimmingDistanceMetres) / 1000.0) 
            : 0;

    // Percentage of time spent moving vs total duration
    public double RunningMovingEfficiency => 
        RunningTimeSeconds > 0 ? (RunningMovingTimeSeconds / RunningTimeSeconds) * 100 : 0;
}