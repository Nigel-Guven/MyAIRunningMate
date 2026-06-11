namespace MyAIRunningMate.Domain.Models.ViewObjects;

public class ActivityView
{
    public Guid ActivityId { get; set; }
    public string GarminActivityId { get; set; }
    public DateTime StartTime { get; set; }
    public string ExerciseType { get; set; }
    public double DurationSeconds { get; set; }
    public double? DistanceMetres { get; set; }
    public int AverageHeartRate { get; set; }
    public int MaxHeartRate { get; set; }
    public double? TotalElevationGain { get; set; }
    public double? AverageSecondPerKilometre { get; set; }
    public double? TrainingEffect { get; set; }
}