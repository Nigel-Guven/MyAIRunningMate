namespace MyAIRunningMate.Domain.Models.Activities;

public class LapDto
{
    public Guid LapId { get; set; }
    public int LapNumber { get; set; }
    public double Distance { get; set; }
    public double Duration { get; set; }
    public int AverageHeartRate { get; set; }
}