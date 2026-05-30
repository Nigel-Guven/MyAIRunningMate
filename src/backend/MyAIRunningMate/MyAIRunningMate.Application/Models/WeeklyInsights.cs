namespace MyAIRunningMate.Application.Models;

public class WeeklyInsights
{
    public double RunningTimeVolume { get; set; } = 0.0;
    public double RunningDistanceVolume { get; set; }
    public double SwimmingTimeVolume { get; set; } = 0.0;
    public double SwimmingDistanceVolume { get; set; }
    public double TotalRunningElevationGain { get; set; }
    public int MeanAverageHeartRate { get; set; }
    public int MeanMaxHeartRate { get; set; }
    public double TotalTrainingEffect { get; set; }
    public double MeanTrainingEffect { get; set; }
    public long TotalAchievementCount { get; set; }
    public long TotalPersonalRecordCount { get; set; }
    public int TotalPersonalExercises { get; set; }
    public int TotalGroupExercises { get; set; }
    public IEnumerable<string> Locations { get; set; } 
    public int RestDays { get; set; }
}