namespace MyAIRunningMate.Contracts.Nexus;

public class NexusRequest
{
    public string PrimaryGoal { get; set; } = string.Empty;

    public string ExperienceYears { get; set; } = string.Empty;

    public string RunningLevel { get; set; } = string.Empty;

    public int ScheduleLengthWeeks { get; set; }

    public string PoolAccess { get; set; } = string.Empty;
}
