namespace MyAIRunningMate.Domain.Activities;

public class LapDto
{
    Guid LapId { get; set; }
    int LapNumber { get; set; }
    double Distance { get; set; }
    double Duration { get; set; }
    int AverageHeartRate { get; set; }
}