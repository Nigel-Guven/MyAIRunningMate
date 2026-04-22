namespace MyAIRunningMate.Domain.Platform;

public class StravaActivityDto
{
    public long Id { get; set; }
    public string Name { get; set; }
    public int MovingTime { get; set; }
    public int ElapsedTime { get; set; }
    public double AverageSpeed { get; set; }
    public double MaxSpeed { get; set; }
    public double? AverageHeartrate { get; set; }
}