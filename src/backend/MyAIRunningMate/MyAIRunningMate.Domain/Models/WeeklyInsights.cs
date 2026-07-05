namespace MyAIRunningMate.Domain.Models;

public class WeeklyInsights
{
    /* --- Running Metrics --- */
    public double RunningTimeSeconds { get; init; }
    public double RunningMovingTimeSeconds { get; init; }
    public double RunningDistanceMetres { get; init; }
    
    /* --- Swimming Metrics --- */
    public double SwimmingTimeSeconds { get; init; }
    public double SwimmingDistanceMetres { get; init; }
    
    /* --- Other Exercise Metrics --- */
    public List<string> OtherTypes { get; init; }
    public double OtherTypesDistanceMetres { get; init; }
    public double OtherTypesTimeSeconds { get; init; }
    
    /* --- Health & Training Scores --- */
    public int TotalCaloriesBurned { get; init; }
    public double TotalTrainingScore { get; init; }
    public double TrainingConsistencyScore { get; init; }
    
    /* --- Activity Distribution --- */
    public int MorningActivities { get; init; }
    public int AfternoonActivities { get; init; }
    public int EveningActivities { get; init; }
    public int NightActivities { get; init; }
    
    /* --- Meta --- */
    public IEnumerable<string> Locations { get; init; } = Enumerable.Empty<string>();
    public int RestDays { get; init; }
    
    /* --- Derived Insights --- */
    
    public double RunningMovingEfficiency => 
        RunningTimeSeconds > 0 ? (RunningMovingTimeSeconds / RunningTimeSeconds) * 100 : 0;
    
    public double PausedSeconds => RunningTimeSeconds - RunningMovingTimeSeconds; 
    public double BodyBatteryDepletion { get; init; }
    public double BodyBatteryEfficiency { get; init; }
    public double RecoveryTimeGenerated { get; init; }
    public double HeartRateIntensityScore { get; init; }
    public double VolumetricOxygenMaxTrend { get; init; }
    public double VolumetricOxygenMaxDiffPercent { get; init; }
    public double RunningEconomyIndex { get; init; }
    
}