using System.Text.Json.Serialization;

namespace MyAIRunningMate.Contracts.Nexus;

public class NexusRequest
{
    [JsonPropertyName("primary_goal")]
    public string PrimaryGoal { get; set; }
    
    [JsonPropertyName("training_plan_length")]
    public string TrainingPlanLength { get; set; }
    
    [JsonPropertyName("pool_size")]
    public string PoolSize { get; set; }
}