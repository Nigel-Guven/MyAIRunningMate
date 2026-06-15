namespace MyAIRunningMate.Domain.Models;

public record AggregateArtifact
{
    public Activity GarminActivity { get; init; }
    public IEnumerable<Lap> Laps { get; init; }
    
    public AggregateArtifact(
        Activity activity, 
        IEnumerable<Lap> laps)
    {
        GarminActivity = activity;
        Laps = laps;
    }
}