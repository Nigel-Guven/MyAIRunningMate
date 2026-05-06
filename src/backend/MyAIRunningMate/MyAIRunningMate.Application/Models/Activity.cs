using MyAIRunningMate.Domain.Models;

namespace MyAIRunningMate.Application.Models;

public class Activity
{
    public string GarminActivityId { get; set; }
    public DateTime StartTime { get; set; }
    public string ExerciseType { get; set; }
    public double DurationSeconds { get; set; }
    public double DistanceMetres { get; set; }
    public int AverageHeartRate { get; set; }
    public int MaxHeartRate { get; set; }
    public double? TotalElevationGain { get; set; }
    public double TrainingEffect { get; set; }
    public double AverageSecondPerKilometre { get; set; }
    public IEnumerable<Lap> Laps { get; set; }
}