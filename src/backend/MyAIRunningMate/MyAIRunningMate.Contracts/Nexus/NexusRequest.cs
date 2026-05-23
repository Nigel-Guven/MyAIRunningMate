using System.Text.Json.Serialization;

namespace MyAIRunningMate.Contracts.Nexus;

public class NexusRequest
{
    [JsonPropertyName("primary_goal")]
    public string PrimaryGoal { get; set; } = string.Empty;

    [JsonPropertyName("experience_years")]
    public string ExperienceYears { get; set; } = string.Empty;

    [JsonPropertyName("running_level")]
    public string RunningLevel { get; set; } = string.Empty;

    [JsonPropertyName("schedule_length_weeks")]
    public int ScheduleLengthWeeks { get; set; }

    [JsonPropertyName("pool_access")]
    public string PoolAccess { get; set; } = string.Empty;
}
