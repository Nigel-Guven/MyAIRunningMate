using MyAIRunningMate.Domain.Activities;

namespace MyAIRunningMate.Domain.Platform;

public class GarminActivityDto
{
    public long Id { get; set; }
    public DateTime StartTime { get; set; }
    public double Distance { get; set; }
    public List<LapDto> Laps { get; set; }
}