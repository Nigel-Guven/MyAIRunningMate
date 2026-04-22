namespace MyAIRunningMate.Domain.Activities;

public class Activity
{
    public Guid Id { get; set; }
    public ActivityType Type { get; set; }
    public DateTime StartTime { get; set; }
    public TimeSpan Duration { get; set; }
    public double DistanceMeters { get; set; }
    public int? AvgHeartRate { get; set; }
    public int? MaxHeartRate { get; set; }
    public double? AvgPacePerKm { get; set; }
    public double? TrainingEffect { get; set; }
    public double? TotalElevationGain { get; set; }

    public List<LapDto> Laps { get; set; } = new();
}