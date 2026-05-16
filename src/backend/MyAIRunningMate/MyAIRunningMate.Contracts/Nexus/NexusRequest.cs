using System.Text.Json.Serialization;

namespace MyAIRunningMate.Contracts.Nexus;

public class NexusRequest
{
    [JsonPropertyName("primary_goal")]
    public string PrimaryGoal { get; set; }
    
    [JsonPropertyName("running_experience_years")]
    public int RunningExperienceInYears { get; set; }
    
    [JsonPropertyName("running_level")]
    public int RunningLevel { get; set; }
    
    [JsonPropertyName("training_plan_length")]
    public int TrainingPlanLength { get; set; }
    
    [JsonPropertyName("pool_size")]
    public string PoolSize { get; set; }
}