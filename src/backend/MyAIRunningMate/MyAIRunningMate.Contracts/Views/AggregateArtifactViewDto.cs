using MyAIRunningMate.Application.Models.ViewObjects;

namespace MyAIRunningMate.Contracts.Views;

public class AggregateArtifactViewDto
{
    public Guid? ActivityId { get; set; }
    public Guid? ResourceId { get; set; }
    public string GarminActivityId { get; set; }
    public string? StravaId { get; set; }
    public string Name { get; set; }
    public string ExerciseType { get; set; } 
    public DateTime StartTime { get; set; }
    public long? ElapsedTime { get; set; }
    public double? AverageCadence { get; set; }
    public double AverageSecondPerKilometre { get; set; }
    public double? TotalElevationGain { get; set; }
    public double? ElevationLow { get; set; }
    public double? ElevationHigh { get; set; }
    public double DurationSeconds { get; set; }
    public double DistanceMetres { get; set; }
    public int? AverageHeartRate { get; set; }
    public int? MaxHeartRate { get; set; }
    public double TrainingEffect { get; set; }
    public long? AchievementCount { get; set; }
    public long? KudosCount { get; set; }
    public long? AthleteCount { get; set; }
    public long? PersonalRecordCount { get; set; }
    public StravaGeomapView? Map { get; set; }
    public IEnumerable<LapView> Laps { get; set; }
}