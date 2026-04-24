namespace MyAIRunningMate.Domain.Models.Activities;

public class StravaResourceDto
{
    public Guid ResourceId { get; set; }
    public string StravaId { get; set; }
    public string Name { get; set; }
    public long ElapsedTime { get; set; }
    public double DistanceMetres { get; set; }
    public double TotalElevationGain { get; set; }
    public double AverageCadence { get; set; }
    public string Type { get; set; }
    public DateTime StartDate { get; set; }
    public long AchievementCount { get; set; }
    public long KudosCount { get; set; }
    public long AthleteCount { get; set; }
    public long PersonalRecordCount { get; set; }
    public double ElevationLow { get; set; }
    public double ElevationHigh { get; set; }
    public MapDto Map { get; set; }
}