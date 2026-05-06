namespace MyAIRunningMate.Contracts.Views;

public class LapViewDto
{
    public int LapNumber { get; set; }
    public double DistanceMetres { get; set; }
    public double DurationSeconds { get; set; }
    public int AverageHeartRate { get; set; }
}